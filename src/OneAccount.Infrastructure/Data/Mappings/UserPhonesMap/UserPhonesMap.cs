using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneAccount.Domain.Entities.UserPhones;

namespace OneAccount.Infrastructure.Data.Mappings.UserPhonesMap;

public sealed class UserPhonesMap : IEntityTypeConfiguration<UserPhone>
{
    public void Configure(EntityTypeBuilder<UserPhone> builder)
    {
        builder.ToTable("UserPhones");
        builder.HasKey(h => h.Id);

        // === UserId ===
        builder.Property<Guid>("UserId")
            .HasColumnName("UserId")
            .IsRequired();
        builder.HasIndex("UserId");

        builder.Property(p => p.CallingCode)
            .HasColumnName("CallingCode")
            .HasMaxLength(6)
            .IsRequired();

        builder.Property(p => p.RegionCode)
            .HasColumnName("RegionCode")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(p => p.AreaCode)
            .HasColumnName("AreaCode")
            .HasMaxLength(10);

        builder.Property(p => p.PhoneType)
            .HasColumnName("PhoneType")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.E164)
            .HasColumnName("E164")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(p => p.PhoneNumber)
            .HasColumnName("PhoneNumber")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(p => p.IsVerified)
            .HasColumnName("IsVerified")
            .IsRequired();

        builder.Property(p => p.VerifiedAt)
            .HasColumnName("VerifiedAt")
            .HasColumnType("datetimeoffset");

        builder.Property(p => p.IsPrimary)
            .HasColumnName("IsPrimary")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetimeoffset")
            .IsRequired();

        // Evita duplicar o mesmo número para o mesmo usuário
        builder.HasIndex("UserId", "E164").IsUnique();
    }
}
