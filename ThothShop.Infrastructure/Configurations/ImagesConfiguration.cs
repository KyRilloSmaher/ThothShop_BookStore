using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class ImagesConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            // Table name
            builder.ToTable("Images");

            // Primary key
            builder.HasKey(bi => bi.Id);
            builder.Property(bi => bi.Id)
                .ValueGeneratedOnAdd();

            // Properties
            builder.Property(bi => bi.Url)
                .IsRequired()
                .HasMaxLength(500);

            // Relationships with Images
            builder.HasMany(i => i.authorIamges)
                   .WithOne(ai =>ai.Image);

            builder.HasMany(i => i.BookImages)
                   .WithOne(bi =>bi.Image);

            builder.HasOne(i => i.Category)
                  .WithOne(c => c.Icon);

        }
    }
}