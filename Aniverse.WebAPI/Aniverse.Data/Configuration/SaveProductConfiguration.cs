using Aniverse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aniverse.Data.Configuration
{
    public class SaveProductConfiguration : IEntityTypeConfiguration<SaveProduct>
    {
        public void Configure(EntityTypeBuilder<SaveProduct> builder)
        {
            builder.Property(sp => sp.SaveAddDate).HasDefaultValueSql("GETDATE()");
        }
    }
}
