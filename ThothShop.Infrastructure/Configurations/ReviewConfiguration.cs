using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            // Table name
            builder.ToTable("Reviews");

            // Primary key with database-generated sequential GUID
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(r => r.Rating)
                .IsRequired()
                .HasAnnotation("Range", new[] { 1, 5 }); // 1-5 star rating

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            // Relationships
            builder.HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews) 
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(r => r.BookId);
            builder.HasIndex(r => r.UserId);
            builder.HasIndex(r => r.CreatedAt);
            builder.HasIndex(r => r.Rating);

            //**************************************************
            // Add constraint for one review per user per book
            //**************************************************
            builder.HasIndex(r => new { r.UserId, r.BookId })
                .IsUnique();

        }
    }
}