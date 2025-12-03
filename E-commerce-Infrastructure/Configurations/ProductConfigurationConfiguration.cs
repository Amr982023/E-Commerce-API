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
    internal class ProductConfigurationConfiguration : IEntityTypeConfiguration<ProductConfiguration>
    {      
        public void Configure(EntityTypeBuilder<ProductConfiguration> builder)
        {
            builder.HasKey(pc => new { pc.ProductItemId, pc.VariationOptionId });
            builder.Property(pc => pc.ProductItemId).IsRequired();
            builder.Property(pc => pc.VariationOptionId).IsRequired();


            builder.HasOne(pc => pc.ProductItem)
                   .WithMany(pi => pi.Configurations)
                   .HasForeignKey(pc => pc.ProductItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pc => pc.VariationOption)
                     .WithMany(vo => vo.Configurations)
                     .HasForeignKey(pc => pc.VariationOptionId)
                     .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
