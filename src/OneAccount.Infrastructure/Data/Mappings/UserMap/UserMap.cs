using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneAccount.Domain.Entities.User;
using OneAccount.Domain.Enumerators;
using OneAccount.Domain.ValueObjects.Accounts;

namespace OneAccount.Infrastructure.Data.Mappings.UserMap;

public sealed class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(h => h.Id);

        // ===== Email (VO) =====
        builder.OwnsOne(o => o.Email, email =>
        {
            email.Property(p => p.EmailAddress)
                .HasColumnName("Email")
                .HasMaxLength(254)
                .IsRequired();
        });

        builder.Navigation(n => n.Email)
            .IsRequired();

        // ===== PasswordHash (VO) =====
        builder.OwnsOne(o => o.PasswordHash, pw =>
        {
            pw.Property(p => p.Password)
              .HasColumnName("PasswordHash")
              .HasMaxLength(256) // ajuste conforme tamanho do hash
              .IsRequired();
        });

        builder.Navigation(n => n.PasswordHash).IsRequired();


        // ===== UserName (VO) =====
        builder.OwnsOne(o => o.UserName, name =>
        {
            name.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Navigation(n => n.UserName)
            .IsRequired();

        // ===== BirthDate (VO) =====
        builder.OwnsOne(o => o.BirthDate, bd =>
        {
            bd.Property(p => p.Value)
              .HasColumnName("BirthDate")
              .HasColumnType("date")
              .IsRequired();
        });

        builder.Navigation(n => n.BirthDate).IsRequired();

        // ===== Status (enum) =====
        builder.Property(p => p.Status)
            .HasColumnName("Status")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        // ===== SuspensionInfo (nullable) =====
        builder.OwnsOne(typeof(SuspensionInfo),"_suspension",si =>
        {
            si.Property(nameof(SuspensionInfo.Reason))
            .HasColumnName("SuspensionReason")
            .HasConversion<int>();

            si.Property(nameof(SuspensionInfo.By))
            .HasColumnName("SuspensionBy")
            .HasConversion<int>();

            si.Property(nameof(SuspensionInfo.SuspendedAt))
            .HasColumnName("SuspendedAt");

            si.Property(nameof(SuspensionInfo.SuspendedUntil))
            .HasColumnName("SuspendedUntil");

            si.Property(nameof(SuspensionInfo.Note))
            .HasColumnName("SuspensionNote")
            .HasMaxLength(500);
        });

        builder.Navigation("_suspension")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // ===== CreatedAt =====
        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        // ===== Documents (OwnsMany) =====
        builder.OwnsMany(o => o.Documents, doc =>
        {
            doc.ToTable("UserDocuments");
            doc.HasKey("Id");

            doc.WithOwner().HasForeignKey("UserId");

            doc.Property<DocumentType>("_documentType")
                .HasColumnName("DocumentType")
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            doc.Property(p => p.DocumentNumber)
                .HasColumnName("DocumentNumber")
                .HasMaxLength(50)
                .IsRequired();

            doc.Property(p => p.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();
        });

        builder.Navigation(n => n.Documents)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
