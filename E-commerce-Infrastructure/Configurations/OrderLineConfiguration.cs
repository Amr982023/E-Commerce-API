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
    internal class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(ol => ol.Id);
            builder.Property(ol => ol.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(ol => ol.Qty).IsRequired();
            builder.Property(ol=>ol.ProductItem).IsRequired();
            builder.Property(ol => ol.ShopOrderId).IsRequired();


            builder.HasOne(ol=>ol.ProductItem)
                .WithMany(pi=>pi.OrderLines)
                .HasForeignKey(ol=>ol.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(ol=>ol.ShopOrder)
                .WithMany(so=>so.OrderLines)
                .HasForeignKey(ol=>ol.ShopOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ol=>ol.UserReviews)
                .WithOne(ur=>ur.OrderLine)
                .HasForeignKey(ur=>ur.OrderedProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
