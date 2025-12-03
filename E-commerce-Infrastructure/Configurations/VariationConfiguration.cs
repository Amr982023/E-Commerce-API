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
    internal class VariationConfiguration : IEntityTypeConfiguration<Variation>
    {
        public void Configure(EntityTypeBuilder<Variation> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Name).IsRequired().HasMaxLength(100);
            builder.Property(v => v.CategoryId).IsRequired();

            builder.HasOne(v => v.Category)
                   .WithMany(c => c.Variations)
                   .HasForeignKey(v => v.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(v => v.Options)
                     .WithOne(o => o.Variation)
                     .HasForeignKey(o => o.VariationId)
                     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
