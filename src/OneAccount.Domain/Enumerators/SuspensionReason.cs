namespace OneAccount.Domain.Enumerators;

public enum SuspensionReason
{
    Unknown,         // Desconhecido
    PaymentFailure,  // Falha no pagamento
    PolicyViolation, // Violação de políticas
    FraudRisk,       // Risco de fraude
    UserRequested,   // Solicitação do usuário
    Inactivity       // Inatividade
}
