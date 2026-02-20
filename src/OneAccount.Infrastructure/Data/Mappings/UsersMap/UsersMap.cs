using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneAccount.Domain.Entities.Users;

namespace OneAccount.Infrastructure.Data.Mappings.UsersMap;

public sealed class UsersMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(h => h.Id);

        builder.OwnsOne(o => o.UserName, name =>
        {
            name.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Navigation(n => n.UserName)
            .IsRequired();

        builder.OwnsOne(o => o.Email, email =>
        {
            email.Property(p => p.EmailAddress)
                .HasColumnName("Email")
                .HasMaxLength(254)
                .IsRequired();
        });

        builder.Navigation(n => n.Email)
            .IsRequired();

        builder.OwnsMany(u => u.Documents, doc =>
        {
            doc.ToTable("UserDocuments");
            doc.HasKey("Id");

            doc.WithOwner().HasForeignKey("UserId");

            doc.Property(d => d.DocumentType)
                .HasColumnName("DocumentType")
                .IsRequired();

            doc.Property(d => d.DocumentNumber)
                .HasColumnName("DocumentNumber")
                .HasMaxLength(50)
                .IsRequired();

            doc.Property(d => d.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();
        });

        // importante para backing field (private list)
        builder.Navigation(u => u.Documents)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasColumnName("IsActive")
            .IsRequired();
    }
}
