using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe.Persistance.EFConfigurations
{
    internal sealed class CafeConfiguration : IEntityTypeConfiguration<Cafe.Domain.Entities.Cafe>
    {
        public void Configure(EntityTypeBuilder<Cafe.Domain.Entities.Cafe> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Location)
               .IsRequired()
               .HasMaxLength(100);
        }
    }
}
