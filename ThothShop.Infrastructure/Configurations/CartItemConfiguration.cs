using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            // Table name
            builder.ToTable("CartItems");

            // Composite key 
            builder.HasKey(ci => new { ci.CartId, ci.BookId });

            // Properties
            builder.Property(ci => ci.Quantity)
                .IsRequired()
                .HasDefaultValue(1)
                .HasAnnotation("Range", new[] { 1, 100 });

            // Relationships
            builder.HasOne(ci => ci.Book)
                .WithMany(o => o.CartItems)
                .HasForeignKey(oi => oi.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Cart)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.CartId)
                .OnDelete(DeleteBehavior.Cascade); // Delete items when order is deleted


            builder.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");
               
            // Indexes
            builder.HasIndex(oi => oi.BookId);
            builder.HasIndex(oi => oi.CartId);
        }
    }
}
