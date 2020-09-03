using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QHomeGroup.Data.Entities.Introduce;

namespace QHomeGroup.Data.EF.Configurations
{
    public class IntroduceConfiguration : IEntityTypeConfiguration<IntroduceConfig>
    {
        public void Configure(EntityTypeBuilder<IntroduceConfig> builder)
        {
            builder.ToTable("IntroduceConfigs");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Image).HasMaxLength(255).IsRequired();
        }
    }
}
