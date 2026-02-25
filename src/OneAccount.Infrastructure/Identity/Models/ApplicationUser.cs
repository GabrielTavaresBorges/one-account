using Microsoft.AspNetCore.Identity;

namespace OneAccount.Infrastructure.Identity.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public Guid DomainUserId { get; set; }
}
