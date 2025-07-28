using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Table name
            builder.ToTable("OrderItems");

            // Primary key with database-generated sequential GUID
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(oi => oi.Quantity)
                .IsRequired()
                .HasDefaultValue(1)
                .HasAnnotation("Range", new[] { 1, 100 });

            builder.Property(oi => oi.UnitPrice)
            .HasColumnType("decimal(18,2)");
            // Relationships
            builder.HasOne(oi => oi.Book)
                .WithMany(o => o.OrderItems) 
                .HasForeignKey(oi => oi.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Delete items when order is deleted

            
        

            // Indexes
            builder.HasIndex(oi => oi.BookId);
            builder.HasIndex(oi => oi.OrderId);

        }
    }
}