using OneAccount.Domain.Abstraction;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.ValueObjects.Emails;

namespace OneAccount.Domain.Entities.EmailConfirmationSettings;

public sealed class EmailConfirmationSetting : Entity, IAggregateRoot
{
    private Email _fromEmail = null!;
    private string _fromName = null!;
    private string _subject = null!;
    private string _bodyHtml = null!;
    private bool _isActive;
    private DateTimeOffset _updatedAt;

    public Email FromEmail => _fromEmail;
    public string FromName => _fromName;
    public string Subject => _subject;
    public string BodyHtml => _bodyHtml;
    public bool IsActive => _isActive;
    public DateTimeOffset UpdatedAt => _updatedAt;

    private EmailConfirmationSetting() { }

    private EmailConfirmationSetting(
        Email fromEmail,
        string fromName,
        string subject,
        string bodyHtml,
        bool isActive)
    {
        _fromEmail = fromEmail;
        _fromName = fromName;
        _subject = subject;
        _bodyHtml = bodyHtml;
        _isActive = isActive;
        _updatedAt = DateTimeOffset.UtcNow;
    }

    public static EmailConfirmationSetting Create(
        Email fromEmail,
        string fromName,
        string subject,
        string bodyHtml,
        bool isActive = true)
    {
        Validate(fromEmail, fromName, subject, bodyHtml);

        return new EmailConfirmationSetting(
            fromEmail,
            fromName.Trim(),
            subject.Trim(),
            bodyHtml.Trim(),
            isActive);
    }

    public void Update(
        Email fromEmail,
        string fromName,
        string subject,
        string bodyHtml,
        bool isActive)
    {
        Validate(fromEmail, fromName, subject, bodyHtml);

        _fromEmail = fromEmail;
        _fromName = fromName.Trim();
        _subject = subject.Trim();
        _bodyHtml = bodyHtml.Trim();
        _isActive = isActive;
        _updatedAt = DateTimeOffset.UtcNow;
    }

    private static void Validate(
        Email fromEmail,
        string fromName,
        string subject,
        string bodyHtml)
    {
        if (fromEmail is null)
            throw new DomainException(
                message: "From email cannot be null.",
                identifier: "EMAIL_CONFIRMATION_SETTING_FROM_EMAIL_NULL");

        if (string.IsNullOrWhiteSpace(fromName))
            throw new DomainException(
                message: "From name cannot be null or empty.",
                identifier: "EMAIL_CONFIRMATION_SETTING_FROM_NAME_EMPTY");

        if (string.IsNullOrWhiteSpace(subject))
            throw new DomainException(
                message: "Subject cannot be null or empty.",
                identifier: "EMAIL_CONFIRMATION_SETTING_SUBJECT_EMPTY");

        if (string.IsNullOrWhiteSpace(bodyHtml))
            throw new DomainException(
                message: "Body HTML cannot be null or empty.",
                identifier: "EMAIL_CONFIRMATION_SETTING_BODY_EMPTY");

        if (fromName.Trim().Length > 150)
            throw new DomainException(
                message: "From name cannot be longer than 150 characters.",
                identifier: "EMAIL_CONFIRMATION_SETTING_FROM_NAME_TOO_LONG");

        if (subject.Trim().Length > 200)
            throw new DomainException(
                message: "Subject cannot be longer than 200 characters.",
                identifier: "EMAIL_CONFIRMATION_SETTING_SUBJECT_TOO_LONG");
    }
}