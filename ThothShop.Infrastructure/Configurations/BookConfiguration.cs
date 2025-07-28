using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Table configuration
            builder.ToTable("Books");

      

            // Book-specific properties
            builder.Property(b => b.Stock)
                .HasDefaultValue(0);

            builder.HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.OrderItems)
                .WithOne(oi => oi.Book)
                .HasForeignKey(oi => oi.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Wishlist)
                .WithOne(w => w.Book)
                .HasForeignKey(w => w.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            //// TPH Discriminator
            //builder.HasDiscriminator<string>("BookType")
            //    .HasValue<Book>("Standard")
            //    .HasValue<UsedBook>("Used");
        }

    }
}