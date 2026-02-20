using OneAccount.Domain.Abstraction;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.Entities.UserDocuments;
using OneAccount.Domain.ValueObjects.Documents;
using OneAccount.Domain.ValueObjects.Emails;
using OneAccount.Domain.ValueObjects.Names;

namespace OneAccount.Domain.Entities.Users;

public class User : Entity, IAggregateRoot
{
    private UserName _userName = null!;
    private Email _emailAddress = null!;
    private readonly List<UserDocument> _documents = new();

    public UserName UserName => _userName;
    public Email Email => _emailAddress;
    public IReadOnlyCollection<UserDocument> Documents => _documents.AsReadOnly();
    public DateTimeOffset CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private User() { }

    private User(UserName userName, Email email)
    {
        _userName = userName;
        _emailAddress = email;

        CreatedAt = DateTimeOffset.UtcNow;
        IsActive = true;
    }

    public static User Create(UserName userName, Email emailAddress, Cpf cpfNumber)
    {
        if (userName is null)
            throw new DomainException(
                message: "UserName cannot be null.", identifier: "USER_NAME_NULL");

        if (emailAddress is null)
            throw new DomainException(
                message: "Email Address cannot be null.", identifier: "EMAIL_NULL");

        if (cpfNumber is null)
            throw new DomainException(
                message: "Cpf number cannot be null.", identifier: "CPF_NULL");

        var user = new User(userName, emailAddress);

        user._documents.Add(UserDocument.CreateFromCpf(cpfNumber));

        return user;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public void ChangeUserName(UserName userName)
    {
        if (userName is null)
            throw new DomainException(
                message: "UserName cannot be null.", identifier: "USER_NAME_NULL");

        _userName = userName;
    }

    public void ChangeEmail(Email email)
    {
        if (email is null)
            throw new DomainException(
                message: "Email cannot be null.", identifier: "EMAIL_NULL");

        _emailAddress = email;
    }
}
