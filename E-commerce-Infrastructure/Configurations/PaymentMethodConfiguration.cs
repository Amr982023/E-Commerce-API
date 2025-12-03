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
    internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);
            builder.Property(pm => pm.AccountNumber).IsRequired().HasMaxLength(50);
            builder.Property(pm => pm.Provider).IsRequired().HasMaxLength(100);
            builder.Property(pm => pm.ExpiryDate).IsRequired();
            builder.Property(pm => pm.IsDefault).IsRequired();
            builder.Property(pm => pm.PaymentTypeId).IsRequired();
            builder.Property(pm => pm.AccountId).IsRequired();


            builder.HasOne(pm => pm.PaymentType)
                .WithMany(pt => pt.PaymentMethods)
                .HasForeignKey(pm => pm.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(pm=>pm.Account)
                .WithMany(a=>a.PaymentMethods)
                .HasForeignKey(pm=>pm.AccountId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(pm=>pm.ShopOrders)
                .WithOne(so=>so.PaymentMethod)
                .HasForeignKey(so => so.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
