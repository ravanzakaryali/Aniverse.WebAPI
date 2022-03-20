using Aniverse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aniverse.Data.Configuration
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.Property(p => p.CreationDate).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.IsOfficial).HasDefaultValue(false);
        }
    }
}
