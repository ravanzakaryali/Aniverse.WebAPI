using Aniverse.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p=>p.CreationDate).HasDefaultValueSql("GETDATE()");
            builder.Property(p=>p.Content).IsRequired();
            builder.HasOne(p => p.Animal).WithMany(b => b.Posts)
                  .HasForeignKey(p => p.AnimalId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
