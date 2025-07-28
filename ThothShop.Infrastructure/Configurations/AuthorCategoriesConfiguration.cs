using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class AuthorCategoriesConfiguration : IEntityTypeConfiguration<AuthorCategories>
    {
        public void Configure(EntityTypeBuilder<AuthorCategories> builder)
        {
            // Table name
            builder.ToTable("AuthorCategories");

            // Composite primary key
            builder.HasKey(ac => new { ac.AuthorId, ac.CategoryId });

            // Configure Author relationship
            builder.HasOne(ac => ac.Author)
                .WithMany(a => a.authorCategories)
                .HasForeignKey(ac => ac.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Category relationship
            builder.HasOne(ac => ac.Category)
                .WithMany(ac=>ac.authorCategories)
                .HasForeignKey(ac => ac.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
          

        }
    }
}