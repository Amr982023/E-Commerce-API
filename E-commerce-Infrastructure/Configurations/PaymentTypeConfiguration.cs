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
    internal class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(pt => pt.Id);
            builder.Property(pt => pt.Type).IsRequired().HasMaxLength(50);

            builder.HasMany(pt => pt.PaymentMethods)
                   .WithOne(pm => pm.PaymentType)
                   .HasForeignKey(pm => pm.PaymentTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
