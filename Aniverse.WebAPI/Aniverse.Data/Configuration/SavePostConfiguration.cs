using Aniverse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Data.Configuration
{
    public class SavePostConfiguration : IEntityTypeConfiguration<SavePost>
    {
        public void Configure(EntityTypeBuilder<SavePost> builder)
        {
            builder.Property(sp => sp.CreationDate).HasDefaultValueSql("GETDATE()");
            builder.Property(sp => sp.UserId).IsRequired();
            builder.Property(sp => sp.UserId).IsRequired();
        }
    }
}
