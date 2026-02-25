using OneAccount.Domain.Abstraction;
using OneAccount.Domain.Abstraction.Exceptions;
using OneAccount.Domain.Enumerators;
using OneAccount.Domain.ValueObjects.Documents;

namespace OneAccount.Domain.Entities.UserDocuments;

public sealed class UserDocument : Entity
{
    private DocumentType _documentType;
    private string _documentNumber = string.Empty;

    public DocumentType DocumentType => _documentType;
    public string DocumentNumber => _documentNumber;
    public DateTimeOffset CreatedAt { get; private set; }

    private UserDocument() { }

    private UserDocument(DocumentType documentType, string documentNumber)
    {
        _documentType = documentType;
        _documentNumber = documentNumber;

        CreatedAt = DateTimeOffset.UtcNow;
    }

    internal static UserDocument CreateFromCpf(Cpf cpf)
    {
        if (cpf is null)
            throw new DomainException(message:"Cpf cannot be null.", identifier:"CPF_NULL");

        return new UserDocument(DocumentType.Cpf, cpf.Number);
    }
}
