using LocalFriendzApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalFriendzApi.Infrastructure.Data.Mappings
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("TB_CONTACT");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("id_contact");

            builder.Property(c => c.Name)
                .IsRequired(true)
                .HasColumnName("name")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

            builder.Property(c => c.Phone)
                .IsRequired(true)
                .HasColumnName("phone")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

            builder.Property(c => c.Email)
                .IsRequired(true)
                .HasColumnName("email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(40);

            builder.Property(c => c.AreaCodeId)
                   .HasColumnName("fk_id_area_code");
        }
    }
}
