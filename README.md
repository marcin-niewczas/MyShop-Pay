<div align="center"> 
  <h3>myShop Pay</h3>
  <h6>Mock Payment Gateway<h6>
</div>

## About The Project
The project is mock payment gateway, created for **myShop** project.
### Built with
* .NET 8
* ASP.NET Core Blazor
* MudBlazor - Blazor Component Library
* ASP.NET Core Web API
* Entity Framework Core (MS SQL)
## Related Projects
* **[myShop API](https://github.com/marcin-niewczas/MyShop-API)**
* **[myShop Angular Client](https://github.com/marcin-niewczas/MyShop-Angular-Client)**

## Getting Started
> [!Important]
> For fully functionality, the project **myShop Pay** must have **[related projects](#related-projects)** running.
> 
> Full instruction for starting projects **myShop** is **[here](https://github.com/marcin-niewczas/MyShop-API#launch-myshop-projects)**.
1. Clone repository
   ```sh
   git clone https://github.com/marcin-niewczas/MyShop-Pay.git
   ```
2. Database
   - Windows
     - Nothing to do, but if you wanna run database via **Docker** go to `Mac OS/Linux` step
   - Mac OS/Linux
     - Go to `./MyShopPay/appsettings.json` and comment `WindowsConnectionString`, then uncomment `DockerConnectionString`
     - Run **Docker App**
     - In root directory of repository run
       ```sh
       docker compose up
       ```
3. In root directory of repository run
   ```sh
   dotnet run --project ./MyShopPay/MyShopPay.csproj --launch-profile https
   ```
