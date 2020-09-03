using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QHomeGroup.Data.EF.Extensions;
using QHomeGroup.Data.Entities.ECommerce;

namespace QHomeGroup.Data.EF.Configurations
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.Property(c => c.TagId).HasMaxLength(50).IsRequired()
                .HasMaxLength(50).IsUnicode(false);
            // etc.
        }
    }
}