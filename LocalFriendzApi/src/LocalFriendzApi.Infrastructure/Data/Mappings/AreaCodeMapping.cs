using LocalFriendzApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalFriendzApi.Infrastructure.Data.Mappings
{
    public class AreaCodeMapping : IEntityTypeConfiguration<AreaCode>
    {
        public void Configure(EntityTypeBuilder<AreaCode> builder)
        {
       
            builder.HasKey(c => c.IdAreaCode);
            builder.Property(c => c.IdAreaCode);
            builder.Property(c => c.CodeRegion)
                .IsRequired(true)
                .HasMaxLength(2);
        }
    }
}
