namespace OneAccount.Domain.Events.Users;

public sealed record UserRegisteredDomainEvent(Guid userId, string email, string userName);