using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QHomeGroup.Data.EF.Extensions;
using QHomeGroup.Data.Entities.Content;

namespace QHomeGroup.Data.EF.Configurations
{
    public class BlogTagConfiguration : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.Property(c => c.TagId).HasMaxLength(50).IsRequired()
                .IsUnicode(false).HasMaxLength(50);
            // etc.
        }
    }
}