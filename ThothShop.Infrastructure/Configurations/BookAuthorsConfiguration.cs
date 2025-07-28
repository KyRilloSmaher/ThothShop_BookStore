using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class BookAuthorsConfiguration : IEntityTypeConfiguration<BookAuthors>
    {
        public void Configure(EntityTypeBuilder<BookAuthors> builder)
        {
            // Table name
            builder.ToTable("BookAuthors");

            // Composite primary key
            builder.HasKey(ba => new { ba.BookId, ba.AuthorId });

            // Configure Book relationship
            builder.HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Author relationship
            builder.HasOne(ba => ba.Author)
                .WithMany(a => a.bookAuthors)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}