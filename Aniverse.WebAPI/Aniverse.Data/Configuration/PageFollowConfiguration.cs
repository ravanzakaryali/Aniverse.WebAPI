using Aniverse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aniverse.Data.Configuration
{
    public class PageFollowConfiguration : IEntityTypeConfiguration<PageFollow>
    {
        public void Configure(EntityTypeBuilder<PageFollow> builder)
        {
            builder.Property(p => p.FollowDate).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(p => p.PageId).IsUnique(false);
        }
    }
}
