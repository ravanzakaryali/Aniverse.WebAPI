using Aniverse.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()");
            builder.Property(p=>p.Name).IsRequired();

        }
    }
}
