using LocalFriendzApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalFriendzApi.Infrastructure.Data.Mappings
{
    public class AreaCodeMapping : IEntityTypeConfiguration<AreaCode>
    {
        public void Configure(EntityTypeBuilder<AreaCode> builder)
        {
            builder.ToTable("TB_AREA_CODE");
            builder.HasKey(c => c.IdAreaCode);

            builder.Property(c => c.IdAreaCode)
                .HasColumnName("id_area_code");

            builder.Property(c => c.CodeRegion)
                .IsRequired(true)
                .HasColumnName("code_region")
                .HasColumnType("int")
                .HasMaxLength(2);
        }
    }
}
