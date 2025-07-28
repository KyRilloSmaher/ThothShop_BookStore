using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Enums;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class UsedBookConfiguration : IEntityTypeConfiguration<UsedBook>
    {
        public void Configure(EntityTypeBuilder<UsedBook> builder)
        {
            // Table Name configuration 
            builder.ToTable("UsedBooks");

            // Inherits all Book configurations
            builder.HasBaseType<BookBase>();

            // UsedBook-specific properties
            builder.Property(ub => ub.Condition)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(ub => ub.Comment)
                .HasMaxLength(1000);

            builder.Property(ub => ub.Note_WayOfConnect)
                .HasMaxLength(500);

            // Relationship with User
            builder.HasOne(ub => ub.user)
                .WithMany(u => u.usedBooks)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}