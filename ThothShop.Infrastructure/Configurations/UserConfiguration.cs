using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            // Properties
            builder.Property(u => u.Address)
                .HasMaxLength(200);
        
            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(11);

            // Encrypted column configuration
            builder.Property(u => u.Code)
                .HasMaxLength(100)
                .HasConversion(
                    v => v, // No conversion needed when using EncryptColumn attribute
                    v => v);

            // Relationships
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Wishlists)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.usedBooks)
                .WithOne(ub => ub.user)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Restrict);

         
             builder.HasMany(u => u.UserRefreshTokens)
             .WithOne(urt => urt.user)
             .HasForeignKey(urt => urt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.PhoneNumber);

            // Configure Identity base properties
            builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
        }
    }
}