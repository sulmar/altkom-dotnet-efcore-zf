using Altkom.ZF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altkom.ZF.DbServices
{

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           builder
                .Property(p=>p.Number)
                .HasMaxLength(10)
                .IsRequired()
                .IsUnicode(true);
        }
    }
}