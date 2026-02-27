using OneAccount.Domain.Abstraction;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.Entities.UserDocuments;
using OneAccount.Domain.Enumerators;
using OneAccount.Domain.ValueObjects.Accounts;
using OneAccount.Domain.ValueObjects.Dates;
using OneAccount.Domain.ValueObjects.Documents;
using OneAccount.Domain.ValueObjects.Emails;
using OneAccount.Domain.ValueObjects.Names;
using OneAccount.Domain.ValueObjects.Security;

namespace OneAccount.Domain.Entities.User;

public class User : Entity, IAggregateRoot
{
    private Email _email = null!;
    private UserName _userName = null!;
    private PasswordHash _passwordHash = null!;
    private BirthDate _birthDate = null!;
    private AccountStatus _status;
    private Gender _gender;
    private SuspensionInfo? _suspension;

    private readonly List<UserDocument> _documents = new();

    public Email Email => _email;
    public PasswordHash PasswordHash => _passwordHash;
    public UserName UserName => _userName;
    public BirthDate BirthDate => _birthDate;
    public AccountStatus Status => _status;
    public Gender Gender => _gender;
    public SuspensionInfo? SuspensionInfo => _suspension;
    public DateTimeOffset CreatedAt { get; private set; }

    public IReadOnlyCollection<UserDocument> Documents => _documents.AsReadOnly();

    private User() { }

    private User(
        Email email,
        PasswordHash passwordHash,
        UserName userName,
        BirthDate birthDate,
        Gender gender)
    {
        _email = email;
        _passwordHash = passwordHash;
        _userName = userName;
        _birthDate = birthDate;
        _gender = gender;

        _status = AccountStatus.Active;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static User Create(
        Email emailAddress,
        PasswordHash passwordHash,
        UserName userName,
        Cpf cpfNumber,
        BirthDate birthDate,
        Gender gender)
    {
        if (emailAddress is null)
            throw new DomainException(message:"Email Address cannot be null.", identifier:"EMAIL_NULL");

        if (passwordHash is null)
            throw new DomainException(message:"Password cannot be null.", identifier:"PASSWORD_HASH_NULL");

        if (userName is null)
            throw new DomainException(message:"UserName cannot be null.", identifier:"USER_NAME_NULL");

        if (cpfNumber is null)
            throw new DomainException(message:"Cpf number cannot be null.", identifier:"CPF_NULL");

        if (birthDate is null)
            throw new DomainException(message:"Birth date cannot be null.", identifier:"BIRTH_DATE_NULL");

        if (gender == Gender.Unknown)
            throw new DomainException(message: "Gender cannot be unknown.", identifier: "GENDER_UNKNOWN");

        var user = new User(emailAddress, passwordHash, userName, birthDate, gender);        
        user._documents.Add(UserDocument.CreateFromCpf(cpfNumber));

        return user;
    }

    public void ChangeEmail(Email email)
    {
        if (email is null)
            throw new DomainException(message:"Email cannot be null.", identifier:"EMAIL_NULL");

        _email = email;
    }

    public void ChangePasswordHash(PasswordHash passwordHash)
    {
        if (passwordHash is null)
            throw new DomainException(message:"Password hash cannot be null.", identifier:"PASSWORD_HASH_NULL");

        _passwordHash = passwordHash;
    }

    public void ChangeUserName(UserName userName)
    {
        if (userName is null)
            throw new DomainException(message:"UserName cannot be null.", identifier:"USER_NAME_NULL");

        _userName = userName;
    }    

    public void ChangeBirthDate(BirthDate birthDate)
    {
        if (birthDate is null)
            throw new DomainException(message:"Birth date cannot be null.", identifier:"BIRTH_DATE_NULL");

        _birthDate = birthDate;
    }

    public void ChangeGender(Gender gender)
    {
        if (gender == Gender.Unknown)
            throw new DomainException(
                message: "Gender cannot be unknown.",
                identifier: "GENDER_UNKNOWN");

        if (_gender == gender)
            return;

        _gender = gender;
    }
}
