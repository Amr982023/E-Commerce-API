using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_commerce_Infrastructure.Configurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UnitNumber).HasMaxLength(20);
            builder.Property(a => a.Street).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Region).IsRequired().HasMaxLength(50);
            builder.Property(a => a.City).IsRequired().HasMaxLength(50);
            builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(20);
            builder.Property(a => a.IsDefault).IsRequired();
            builder.Property(a => a.CountryId).IsRequired();
            builder.Property(a => a.AccountId).IsRequired();


            builder.HasOne(a=>a.Country)
                .WithMany(c=>c.Addresses)
                .HasForeignKey(a=>a.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Account)
                .WithMany(ac => ac.Addresses)
                .HasForeignKey(a => a.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Orders)
                .WithOne(so => so.ShippingAddress)
                .HasForeignKey(so => so.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
