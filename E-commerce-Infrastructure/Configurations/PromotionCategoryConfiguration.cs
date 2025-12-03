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
    internal class PromotionCategoryConfiguration : IEntityTypeConfiguration<PromotionCategory>
    {
        public void Configure(EntityTypeBuilder<PromotionCategory> builder)
        {
            builder.HasKey(pc => new { pc.CategoryId, pc.PromotionId });
            builder.Property(pc => pc.CategoryId).IsRequired();
            builder.Property(pc => pc.PromotionId).IsRequired();

            builder.HasOne(pc => pc.Category)
                   .WithMany(c => c.PromotionCategories)
                   .HasForeignKey(pc => pc.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pc => pc.Promotion)
                     .WithMany(p => p.PromotionCategories)
                     .HasForeignKey(pc => pc.PromotionId)
                     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
