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
    internal class VariationOptionConfiguration : IEntityTypeConfiguration<VariationOption>
    {
        public void Configure(EntityTypeBuilder<VariationOption> builder)
        {
            builder.HasKey(vo => vo.Id);
            builder.Property(vo => vo.Value)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(vo => vo.VariationId).IsRequired();

            builder.HasOne(vo => vo.Variation)
                   .WithMany(v => v.Options)
                   .HasForeignKey(vo => vo.VariationId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
