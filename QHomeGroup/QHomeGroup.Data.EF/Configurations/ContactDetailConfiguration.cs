using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QHomeGroup.Data.EF.Extensions;
using QHomeGroup.Data.Entities.Content;

namespace QHomeGroup.Data.EF.Configurations
{
    public class ContactDetailConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}