using Aniverse.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aniverse.Data.Configuration
{
    public class AnimalFollowConfiguration : IEntityTypeConfiguration<AnimalFollow>
    {
        public void Configure(EntityTypeBuilder<AnimalFollow> builder)
        {
            builder.Property(a=>a.FollowDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
