using Aniverse.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aniverse.Data.Configuration
{
    public class StoryConfigration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.Property(s=>s.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Ignore(s=>s.ImageSrc);
        }
    }
}
