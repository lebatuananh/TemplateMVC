using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QHomeGroup.Data.Entities.Content;

namespace QHomeGroup.Data.EF.Configurations
{
    internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");
            builder.HasKey(c => c.Id);
            builder.HasOne(t => t.AppUser)
                .WithMany(x => x.Blogs)
                .HasForeignKey(x => x.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}