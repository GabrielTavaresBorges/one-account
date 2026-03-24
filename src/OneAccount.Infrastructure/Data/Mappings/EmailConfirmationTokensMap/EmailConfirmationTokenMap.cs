using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneAccount.Domain.Entities.EmailConfirmationTokens;

namespace OneAccount.Infrastructure.Data.Mappings.EmailConfirmationTokensMap;

public sealed class EmailConfirmationTokenMap : IEntityTypeConfiguration<EmailConfirmationToken>
{
    public void Configure(EntityTypeBuilder<EmailConfirmationToken> builder)
    {
        builder.ToTable("EmailConfirmationTokens");
        builder.HasKey(h => h.Id);

        // === UserId ===
        builder.Property(p => p.UserId)
            .HasColumnName("UserId")
            .IsRequired();

        builder.HasIndex(h => h.UserId);

        // ===== GeneratedEmailConfirmationToken (VO) =====
        builder.OwnsOne(o => o.TokenHash, tokenHash =>
        {
            tokenHash.Property(p => p.Token)
                .HasColumnName("TokenHash")
                .HasMaxLength(1024)
                .IsRequired();
        });

        builder.Navigation(n => n.TokenHash)
            .IsRequired();

        // ===== CreatedAt =====
        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        // ===== ExpiresAt =====
        builder.Property(p => p.ExpiresAt)
            .HasColumnName("ExpiresAt")
            .IsRequired();

        // ===== UsedAt =====
        builder.Property(p => p.UsedAt)
            .HasColumnName("UsedAt");

    }
}
