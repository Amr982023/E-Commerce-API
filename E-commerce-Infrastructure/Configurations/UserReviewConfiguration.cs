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
    internal class UserReviewConfiguration : IEntityTypeConfiguration<UserReview>
    {
        public void Configure(EntityTypeBuilder<UserReview> builder)
        {
            builder.HasKey(ur => ur.Id);
            builder.Property(ur => ur.RatingValue).IsRequired();
            builder.Property(ur => ur.UserId).IsRequired();
            builder.Property(ur => ur.OrderedProductId).IsRequired();
            builder.Property(ur => ur.Comment).HasMaxLength(1000);

            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.OrderLine)
                     .WithMany(ol => ol.UserReviews)
                     .HasForeignKey(ur => ur.OrderedProductId)
                     .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
