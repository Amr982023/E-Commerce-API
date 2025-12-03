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
    internal class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            builder.HasKey(sm => sm.Id);
            builder.Property(sm => sm.Name)
                   .IsRequired()
                   .HasColumnType("NVARCHAR(100)");
            builder.Property(sm => sm.Price).HasColumnType("DECIMAL(18,2)").IsRequired();


            builder.HasMany(sm => sm.ShopOrders)
                   .WithOne(so => so.ShippingMethod)
                   .HasForeignKey(so => so.ShippingMethodId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
