using OneAccount.Domain.ValueObjects.Security;

namespace OneAccount.Application.Services.Security;

public sealed record GeneratedEmailConfirmationToken(string rawToken, TokenHash tokenHash);