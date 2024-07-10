using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyShopPay.DataAccessLayer.Models;

namespace MyShopPay.DataAccessLayer.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(e => e.Id);

        builder
            .Property(e => e.Username)
            .IsRequired();

        builder
            .Property(e => e.SecuredPassword)
            .IsRequired();
    }
}
