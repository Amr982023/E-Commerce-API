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
    internal class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(os => os.Id);
            builder.Property(os => os.Status).IsRequired().HasMaxLength(50);

            builder.HasMany(os => os.ShopOrders)
                   .WithOne(so => so.Status)
                   .HasForeignKey(so => so.OrderStatusId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
