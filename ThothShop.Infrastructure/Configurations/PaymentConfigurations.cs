using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ThothShop.Domain.Models;

namespace ThothShop.Infrastructure.Configurations
{
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Table name
            builder.ToTable("Payments");
            builder.HasKey(p => p.Id);


            builder.Property(p => p.OrderId).IsRequired();

            builder.Property(p => p.Amount).IsRequired()
                                           .HasPrecision(18, 2); // 18 digits total, 2 after the decimal point

            builder.Property(p => p.PaymentMethod).IsRequired();
            builder.Property(o => o.PaymentMethod)
              .IsRequired()
              .HasConversion<string>() // Store enum as string
              .HasMaxLength(20);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(o => o.Status)
              .IsRequired()
              .HasConversion<string>() // Store enum as string
              .HasMaxLength(20);
            builder.Property(p => p.TransactionId).IsRequired(false);
            builder.Property(p => p.CreatedAt).IsRequired();

            // Relationships

            builder.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent payment deletion if order exists

        }
    }
   
}
