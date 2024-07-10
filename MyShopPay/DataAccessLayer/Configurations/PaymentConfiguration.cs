using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyShopPay.DataAccessLayer.Models;
using MyShopPay.ValueObjects;

namespace MyShopPay.DataAccessLayer.Configurations;

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder
            .HasIndex(e => e.Id);

        builder
            .Property(e => e.Price)
            .HasPrecision(10, 2)
            .IsRequired();

        builder
            .Property(e => e.ContinueUri)
            .IsRequired();

        builder
           .Property(e => e.CreatedAt)
           .IsRequired();

        builder
            .Property(e => e.Status)
            .HasConversion(v => v.ToString(), v => new PaymentStatus(v))
            .IsRequired();
    }
}
