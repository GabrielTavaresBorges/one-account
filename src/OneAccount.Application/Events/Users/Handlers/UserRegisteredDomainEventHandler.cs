using MediatR;
using Microsoft.Extensions.Logging;
using OneAccount.Application.Events.Notifications;
using OneAccount.Application.Services.Security.Interfaces;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.Entities.EmailConfirmationTokens;
using OneAccount.Domain.Events.Users;
using OneAccount.Domain.Repositories.EmailConfirmationTokensRepository;

namespace OneAccount.Application.Events.Users.Handlers;

public sealed class UserRegisteredDomainEventHandler
    : INotificationHandler<DomainEventNotification<UserRegisteredDomainEvent>>
{
    private readonly IEmailConfirmationTokenService _emailConfirmationTokenService;
    private readonly IEmailConfirmationTokenRepository _emailConfirmationTokenRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly ILogger<UserRegisteredDomainEventHandler> _logger;

    public UserRegisteredDomainEventHandler(
        IEmailConfirmationTokenService emailConfirmationTokenService,
        IEmailConfirmationTokenRepository emailConfirmationTokenRepository,
        IUnityOfWork unityOfWork,
        ILogger<UserRegisteredDomainEventHandler> logger)
    {
        _emailConfirmationTokenService = emailConfirmationTokenService;
        _emailConfirmationTokenRepository = emailConfirmationTokenRepository;
        _unityOfWork = unityOfWork;
        _logger = logger;
    }

    public async Task Handle(
        DomainEventNotification<UserRegisteredDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        try
        {
            var domainEvent = notification.DomainEvent;

            var generatedToken = await _emailConfirmationTokenService.GenerateTokenAsync();

            var expiresAt = DateTimeOffset.UtcNow.AddHours(24);

            var emailConfirmationToken = EmailConfirmationToken.Create(
                userId: domainEvent.UserId,
                tokenHash: generatedToken.tokenHash,
                expiresAt: expiresAt);

            await _emailConfirmationTokenRepository.CreateEmailConfirmationTokenAsync(
                emailConfirmationToken,
                cancellationToken);

            await _unityOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation(
                "Email confirmation token created for user {UserId}.",
                domainEvent.UserId);

            // Futuro passo:
            // enviar email usando generatedToken.rawToken
        }
        catch (DomainException ex)
        {
            _logger.LogError(
                ex,
                "Domain error while creating email confirmation token for user {UserId}.",
                notification.DomainEvent.UserId);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unexpected error while handling UserRegisteredDomainEvent for user {UserId}.",
                notification.DomainEvent.UserId);

            throw;
        }
    }
}