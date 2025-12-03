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
    internal class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(sci => sci.Id);
            builder.Property(sci => sci.Qty).IsRequired();
            builder.Property(sci => sci.ShoppingCartId).IsRequired();
            builder.Property(sci => sci.ProductItemId).IsRequired();


            builder.HasOne(sci => sci.ShoppingCart)
                   .WithMany(sc => sc.Items)
                   .HasForeignKey(sci => sci.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sci => sci.ProductItem)
                     .WithMany(pi => pi.CartItems)
                     .HasForeignKey(sci => sci.ProductItemId)
                     .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
