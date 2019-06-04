using System;
using System.Linq.Expressions;
using Altkom.ZF.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Altkom.ZF.DbServices
{

    public class JsonValueConverter<T> : ValueConverter<T, string>
    {
        public JsonValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<T>(v),
                mappingHints
            )
        {

        }
    }

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

            builder.HasQueryFilter(p=> !p.IsDeleted);

            // dotnet add package Newtonsoft.Json
            // builder.Property(p=>p.ShippingAddress)
            //     .HasConversion(
            //         v => JsonConvert.SerializeObject(v),
            //         v => JsonConvert.DeserializeObject<Address>(v));


            builder.Property(p=>p.ShippingAddress)
                .HasConversion(new JsonValueConverter<Address>());
        
        }
    }
}