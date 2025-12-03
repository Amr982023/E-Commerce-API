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
    internal class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(sc => sc.Id);
            builder.Property(sc => sc.AccountId).IsRequired();


            builder.HasOne(sc => sc.Account)
                   .WithMany(a => a.ShoppingCarts)
                   .HasForeignKey(sc => sc.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(sc => sc.Items)
                     .WithOne(sci => sci.ShoppingCart)
                     .HasForeignKey(sci => sci.ShoppingCartId)
                     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
