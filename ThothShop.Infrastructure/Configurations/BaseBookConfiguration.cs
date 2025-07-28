using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public abstract class BookBaseConfiguration<T> : IEntityTypeConfiguration<BookBase>
    {
        public virtual void Configure(EntityTypeBuilder<BookBase> builder)
        {
            // Primary key with database-generated sequential GUID
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(b => b.Title)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(b => b.Description)
                .HasMaxLength(2000);

            builder.Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.ViewCount)
                .HasDefaultValue(0);

            builder.Property(b => b.PublishedDate)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder.HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.BookImages)
                .WithOne(bi=>bi.Book)
                .HasForeignKey(bi => bi.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Authors)
                .WithOne(ba => ba.Book)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}