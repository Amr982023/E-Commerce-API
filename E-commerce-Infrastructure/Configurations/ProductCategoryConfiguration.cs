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
    internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(pc => pc.Id);
            builder.Property(pc => pc.CategoryName).IsRequired().HasMaxLength(100);


            builder.HasOne(pc => pc.Parent)
                   .WithMany(pc => pc.Children)
                   .HasForeignKey(pc => pc.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pc => pc.Variations)
                     .WithOne(v => v.Category)
                     .HasForeignKey(v => v.CategoryId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pc => pc.Products)
                        .WithOne(p => p.Category)
                        .HasForeignKey(p => p.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
