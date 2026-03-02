using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneAccount.Domain.Entities.UserDocuments;

namespace OneAccount.Infrastructure.Data.Mappings.UserDocuments;

public sealed class UserDocumentsMap : IEntityTypeConfiguration<UserDocument>
{
    public void Configure(EntityTypeBuilder<UserDocument> builder)
    {
        builder.ToTable("UserDocuments");
        builder.HasKey(h => h.Id);

        // === UserId ===
        builder.Property<Guid>("UserId")
            .HasColumnName("UserId")
            .IsRequired();
        builder.HasIndex("UserId");

        builder.Property(p => p.DocumentType)
            .HasColumnName("DocumentType")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.DocumentNumber)
            .HasColumnName("DocumentNumber")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetimeoffset")
            .IsRequired();
    }
}
