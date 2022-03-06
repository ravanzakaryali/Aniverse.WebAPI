using Aniverse.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aniverse.Data.Configuration
{
    public class AnimalCategoryConfiguration : IEntityTypeConfiguration<AnimalCategory>
    {
        public void Configure(EntityTypeBuilder<AnimalCategory> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        }
    }
}
