using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneAccount.Domain.Entities.EmailConfirmationSettings;

namespace OneAccount.Infrastructure.Data.Mappings.EmailConfirmationSettingsMap;

public sealed class EmailConfirmationSettingMap : IEntityTypeConfiguration<EmailConfirmationSetting>
{
    public void Configure(EntityTypeBuilder<EmailConfirmationSetting> builder)
    {
        builder.ToTable("EmailConfirmationSettings");
        builder.HasKey(h => h.Id);

        // ===== FromEmail (VO) =====
        builder.OwnsOne(o => o.FromEmail, email =>
        {
            email.Property(p => p.EmailAddress)
                .HasColumnName("FromEmail")
                .HasMaxLength(254)
                .IsRequired();
        });

        builder.Navigation(n => n.FromEmail)
            .IsRequired();

        // ===== FromName =====
        builder.Property<string>("_fromName")
            .HasColumnName("FromName")
            .HasMaxLength(150)
            .IsRequired();

        // ===== Subject =====
        builder.Property<string>("_subject")
            .HasColumnName("Subject")
            .HasMaxLength(200)
            .IsRequired();

        // ===== BodyHtml =====
        builder.Property<string>("_bodyHtml")
            .HasColumnName("BodyHtml")
            .IsRequired();

        // ===== IsActive =====
        builder.Property<bool>("_isActive")
            .HasColumnName("IsActive")
            .IsRequired();

        // ===== UpdatedAt =====
        builder.Property<DateTimeOffset>("_updatedAt")
            .HasColumnName("UpdatedAt")
            .IsRequired();
    }
}