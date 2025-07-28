using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            // Table name
            builder.ToTable("UserRefreshTokens");

            // Properties
            builder.Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(rt => rt.RefreshToken)
                .IsRequired()
                .HasMaxLength(88); 

            builder.Property(rt => rt.JwtId)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(rt => rt.IsUsed)
                .HasDefaultValue(false);

            builder.Property(rt => rt.IsRevoked)
                .HasDefaultValue(false);

            builder.Property(rt => rt.AddedTime)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(rt => rt.ExpiryDate)
                .IsRequired();

            // Relationships
            builder.HasOne(rt => rt.user)
                .WithMany(u => u.UserRefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete tokens when user is deleted

            // Indexes
            builder.HasIndex(rt => rt.UserId);
            builder.HasIndex(rt => rt.RefreshToken).IsUnique();
            builder.HasIndex(rt => rt.JwtId);
            builder.HasIndex(rt => new { rt.IsUsed, rt.IsRevoked });
            builder.HasIndex(rt => rt.ExpiryDate);
        }
    }
}