using Aniverse.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aniverse.Data.Configuration
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.Ignore(p => p.ImageFile);
            builder.HasOne(p => p.Animal).WithMany(b => b.Pictures)
                   .HasForeignKey(p => p.AnimalId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
