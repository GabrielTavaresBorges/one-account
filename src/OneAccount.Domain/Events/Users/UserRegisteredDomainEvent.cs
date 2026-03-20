using OneAccount.Domain.Abstraction.Events;

namespace OneAccount.Domain.Events.Users;

public sealed record UserRegisteredDomainEvent(
    Guid UserId,
    string Email,
    string UserName) : DomainEvent;