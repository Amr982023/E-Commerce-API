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
    internal class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasColumnType("NVARCHAR(100)");
            builder.Property(p => p.Description).HasColumnType("NVARCHAR(500)");
            builder.Property(p => p.DiscountRate).IsRequired().HasColumnType("DECIMAL(5,2)");
            builder.Property(p => p.StartDate).IsRequired().HasColumnType("DATETIME");
            builder.Property(p => p.EndDate).IsRequired().HasColumnType("DATETIME");
        }
    }
}
