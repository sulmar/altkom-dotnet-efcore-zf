using Altkom.ZF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altkom.ZF.DbServices
{

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
             builder
                .Property(p=>p.FirstName)
                .HasMaxLength(50);

            builder
                .Property(p=>p.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}