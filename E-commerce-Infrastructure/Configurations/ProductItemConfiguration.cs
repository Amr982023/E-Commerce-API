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
    internal class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.HasKey(pi => pi.Id);
            builder.Property(pi => pi.SKU).IsRequired().HasColumnType("NVARCHAR(50)");
            builder.Property(pi => pi.ProductImage).HasMaxLength(200);
            builder.Property(pi => pi.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(pi => pi.QtyInStock).IsRequired();
            builder.Property(pi => pi.ProductId).IsRequired();

            builder.HasOne(pi => pi.Product)
                   .WithMany(p => p.ProductItems)
                   .HasForeignKey(pi => pi.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pi => pi.CartItems)
                     .WithOne(ci => ci.ProductItem)
                     .HasForeignKey(ci => ci.ProductItemId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pi => pi.OrderLines)
                        .WithOne(ol => ol.ProductItem)
                        .HasForeignKey(ol => ol.ProductItemId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
