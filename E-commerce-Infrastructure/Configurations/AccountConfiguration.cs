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
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Password).IsRequired().HasMaxLength(50);
            builder.Property(a=>a.UserId).IsRequired();

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Accounts)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(a=>a.Addresses)
                .WithOne(ad=>ad.Account)
                .HasForeignKey(ad=>ad.AccountId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(a => a.PaymentMethods)
                .WithOne(pm=>pm.Account)
                .HasForeignKey(pm=>pm.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.ShoppingCarts)
                .WithOne(sc => sc.Account)
                .HasForeignKey(sc => sc.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
