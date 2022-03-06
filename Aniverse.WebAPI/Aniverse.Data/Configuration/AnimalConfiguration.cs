using Aniverse.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aniverse.Data.Configuration
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.Property(a=>a.Name).IsRequired();
            builder.Property(a=>a.Birthday);
            builder.Property(a => a.Breed).HasMaxLength(100);
        }
    }
}
