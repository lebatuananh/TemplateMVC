using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QHomeGroup.Data.Entities.Projects;

namespace QHomeGroup.Data.EF.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(c => c.Id);
            builder.HasOne(t => t.AppUser)
             .WithMany(x => x.Projects)
             .HasForeignKey(x => x.CreatedBy)
             .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Service)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
            // etc.
        }
    }
}
