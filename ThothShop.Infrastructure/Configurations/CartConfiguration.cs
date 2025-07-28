using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            // Table name
            builder.ToTable("Carts");

            // Primary key with database-generated sequential GUID
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

        // Properties
        builder.Property(c=>c.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        // Relationships
        builder.HasOne(o => o.User)
                .WithMany(u => u.carts)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent user deletion if orders exist

        builder.HasMany(o => o.Items)
                .WithOne(oi => oi.Cart)
                .HasForeignKey(oi => oi.CartId)
                .OnDelete(DeleteBehavior.Cascade); // Delete items when order is deleted

        // Indexes
        builder.HasIndex(o => o.UserId);
      }
    }
}
