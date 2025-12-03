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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).IsRequired().HasColumnType("NVARCHAR(50)");
            builder.Property(u => u.LastName).IsRequired().HasColumnType("NVARCHAR(50)");
            builder.Property(u => u.FullName).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
            builder.Property(u => u.Phone).IsRequired().HasColumnType("NVARCHAR(15)");
            builder.Property(u => u.Email).IsRequired().HasColumnType("NVARCHAR(100)");

            builder.HasMany(u => u.Accounts)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Orders)
                     .WithOne(o => o.User)
                     .HasForeignKey(o => o.UserId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Reviews)
                        .WithOne(r => r.User)
                        .HasForeignKey(r => r.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
