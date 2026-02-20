using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Domain.ValueObjects.Documents;

public sealed record Cpf
{
    public string Number { get; }
    private Cpf(string number)
    {
        Number = number;
    }
    public static Result<Cpf> Create(string number)
    {
        var validatedNumber = ValidateNumber(number);

        if (validatedNumber.IsFailure)
            return Result<Cpf>.Failure(validatedNumber.Error);

        return Result<Cpf>.Success(new Cpf(validatedNumber.Value));
    }

    private static Result<string> ValidateNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            return Result<string>.Failure(new Error(
             Identifier: "CPF_NUMBER_EMPTY",
             Message: "CPF number cannot be null or empty."));
        }

        var onlyNumbers = ExtractNumbers(number);

        if (!HasValidLength(onlyNumbers))
        {
            return Result<string>.Failure(new Error(
               Identifier: "CPF_NUMBER_INVALID_LENGTH",
               Message: "CPF number must be 11 digits long and contain only numbers."));
        }

        if (!IsValidCnpj(onlyNumbers))
        {
            return Result<string>.Failure(new Error(
                Identifier: "CPF_NUMBER_CHECKSUM_INVALID",
                Message: "CPF failed validation."));
        }

        return Result<string>.Success(onlyNumbers);
    }

    private static string ExtractNumbers(string value) =>
        new string(value.Where(char.IsDigit).ToArray());

    private static bool HasValidLength(string number) =>
        number.Length == 11;

    private static bool IsValidCnpj(string cpfNumber)
    {
        // TODO: Implementar regra real de validação para formato novo e verificar o antigo
        return true;
    }
}
