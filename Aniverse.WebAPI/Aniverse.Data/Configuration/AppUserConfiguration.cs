using Aniverse.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Aniverse.Data.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.RegisterDate).HasDefaultValueSql("GETDATE()");
            builder.Property(u=>u.Firstname).HasMaxLength(128);
            builder.Property(u=>u.Lastname).HasMaxLength(128);
            builder.Property(u=>u.Bio).HasMaxLength(255);
            builder.Property(u=>u.IsBlock).HasDefaultValue(false);

            builder
                .HasMany(u => u.Friends)
                .WithOne(uf => uf.User)
                .HasForeignKey(uf => uf.UserId);
        }
    }
}
