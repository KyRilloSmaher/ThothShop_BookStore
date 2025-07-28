using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table configuration
            builder.ToTable("Categories");

            // Primary key with database-generated sequential GUID
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Property configurations
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
         
            // Relationship configuration
            builder.HasMany(c => c.Books)
                .WithOne(b=>b.Category)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.authorCategories)
                 .WithOne(ac => ac.Category)
                .HasForeignKey(ac => ac.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Icon)
                   .WithOne(i => i.Category)
                   .HasForeignKey<Category>(c => c.IconId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}