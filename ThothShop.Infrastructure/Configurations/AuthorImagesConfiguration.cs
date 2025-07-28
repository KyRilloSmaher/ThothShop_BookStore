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
    public class AuthorImagesConfiguration : IEntityTypeConfiguration<AuthorImages>
    {
        public void Configure(EntityTypeBuilder<AuthorImages> builder)
        {
            // Optional: Table name if needed
            builder.ToTable("AuthorImages");

            // Composite Primary Key
            builder.HasKey(ai => new { ai.AuthorId, ai.ImageId });

            // Relationships
            builder.HasOne(ai => ai.author)
                   .WithMany(a => a.AuthorImages)
                   .HasForeignKey(ai => ai.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ai => ai.Image)
                   .WithMany(i => i.authorIamges)
                   .HasForeignKey(ai => ai.ImageId)
                   .OnDelete(DeleteBehavior.Cascade);

          
        }
    }
}
