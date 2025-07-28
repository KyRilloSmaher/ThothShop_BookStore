using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class BookImagesConfiguration : IEntityTypeConfiguration<BookImages>
    {
        public void Configure(EntityTypeBuilder<BookImages> builder)
        {
            builder.ToTable("BookImages");
            // Composite Primary Key
            builder.HasKey(bi => new { bi.BookId, bi.ImageId });

            // Relationships
            builder.HasOne(bi => bi.Book)
                   .WithMany(b => b.BookImages)
                   .HasForeignKey(bi => bi.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bi => bi.Image)
                   .WithMany(i => i.BookImages)
                   .HasForeignKey(bi => bi.ImageId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
