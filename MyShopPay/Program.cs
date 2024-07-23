using Microsoft.OpenApi.Models;
using MudBlazor.Services;
using MyShopPay.Components;
using MyShopPay.DataAccessLayer;
using MyShopPay.Endpoints;
using MyShopPay.Exceptions;
using MyShopPay.Options;
using MyShopPay.Security;
using MyShopPay.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("en-US");
cultureInfo.NumberFormat.CurrencySymbol = "$";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services
    .AddMudServices();

builder.Services
    .AddSingleton<ExceptionsMiddleware>()
    .AddAppSettingsOptions(builder.Configuration)
    .AddSecurity(builder.Configuration)
    .AddDataAccessLayer()
    .AddServices();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.DescribeAllParametersInCamelCase();

    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "myShop Pay Mock API",
        Version = "v1"
    });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste yours JWT Token",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    Array.Empty<string>()
                }
            });
});


var app = builder.Build();

app.UseExceptionHandler("/Error");
app.UseMiddleware<ExceptionsMiddleware>();
app.UseHsts();


app.UseSecurity();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.MapGroup("/api/v1/payments")
   .MapPaymentEndpoints();

app.MapGroup("/api/v1/users")
   .MapUserEndpoints();

app.Run();
