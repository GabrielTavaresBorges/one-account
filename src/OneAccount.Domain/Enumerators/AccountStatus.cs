namespace OneAccount.Domain.Enumerators;

public enum AccountStatus
{
    Unknown,                  // Desconhecido
    PendingEmailConfirmation, // Pendente de verificação de email
    Active,                   // Ativo 
    Suspended,                // Suspenso
    Disabled                  // Desativado
}
