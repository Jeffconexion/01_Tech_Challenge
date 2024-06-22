using LocalFriendzApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalFriendzApi.Infrastructure.Data.Mappings
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
       
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(c => c.Phone)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(c => c.Email)
                .IsRequired(true)
                .HasMaxLength(40);

            builder.Property(c => c.AreaCodeId);
        }
    }
}
