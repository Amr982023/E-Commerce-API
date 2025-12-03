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
    internal class ShopOrderConfiguration : IEntityTypeConfiguration<ShopOrder>
    {
        public void Configure(EntityTypeBuilder<ShopOrder> builder)
        {
           builder.HasKey(so => so.Id);
            builder.Property(so => so.OrderDate)
                  .IsRequired();
            builder.Property(so => so.OrderTotalCost).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(so => so.UserId).IsRequired();
            builder.Property(so => so.PaymentMethodId).IsRequired();
            builder.Property(so => so.ShippingAddressId).IsRequired();
            builder.Property(so => so.ShippingMethodId).IsRequired();
            builder.Property(so => so.OrderStatusId).IsRequired();

            builder.HasOne(so => so.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(so => so.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(so => so.PaymentMethod)
                     .WithMany(pm => pm.ShopOrders)
                     .HasForeignKey(so => so.PaymentMethodId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(so => so.ShippingAddress)
                        .WithMany(a => a.Orders)
                        .HasForeignKey(so => so.ShippingAddressId)
                        .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(so => so.ShippingMethod)
                            .WithMany(sm => sm.ShopOrders)
                            .HasForeignKey(so => so.ShippingMethodId)
                            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(so => so.Status)
                             .WithMany(os => os.ShopOrders)
                             .HasForeignKey(so => so.OrderStatusId)
                             .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(so => so.OrderLines)
                                .WithOne(ol => ol.ShopOrder)
                                .HasForeignKey(ol => ol.ShopOrderId)
                                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
