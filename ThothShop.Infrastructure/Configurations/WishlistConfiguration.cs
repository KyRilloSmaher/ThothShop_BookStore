using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            // Table name
            builder.ToTable("Wishlists");

            // Primary key with database-generated sequential GUID
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(w => w.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            // Relationships
            builder.HasOne(w => w.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Delete wishlist items when user is deleted

            builder.HasOne(w => w.Book)
                .WithMany(b => b.Wishlist)
                .HasForeignKey(w => w.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite unique constraint (one book per user in wishlist)
            builder.HasIndex(w => new { w.UserId, w.BookId })
                .IsUnique();

            // Indexes
            builder.HasIndex(w => w.UserId);
            builder.HasIndex(w => w.BookId);
            builder.HasIndex(w => w.CreatedAt);

        }
    }
}