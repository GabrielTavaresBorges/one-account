using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Domain.ValueObjects.Documents;

public sealed record Cpf
{
    public string Number { get; private init; }

    private Cpf() { Number = null!; }

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

        if (IsAllSameDigits(onlyNumbers))
        {
            return Result<string>.Failure(new Error(
                Identifier: "CPF_NUMBER_CHECKSUM_INVALID",
                Message: "CPF failed validation."));
        }

        if (!IsValidCpf(onlyNumbers))
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

    private static bool IsAllSameDigits(string digits)
    {         
        return digits.All(c => c == digits[0]);
    }

    /// <summary>
    /// Validação oficial do CPF (dígitos verificadores).
    /// Entrada deve conter exatamente 11 dígitos numéricos.
    /// </summary>
    private static bool IsValidCpf(string cpfNumber)
    {
        // 1º dígito verificador (posição 9)
        int sum1 = 0;
        for (int i = 0; i < 9; i++)
        {
            int digit = cpfNumber[i] - '0';
            sum1 += digit * (10 - i);
        }

        int dv1 = (sum1 * 10) % 11;
        if (dv1 == 10) dv1 = 0;

        if (dv1 != (cpfNumber[9] - '0'))
            return false;

        // 2º dígito verificador (posição 10)
        int sum2 = 0;
        for (int i = 0; i < 10; i++)
        {
            int digit = cpfNumber[i] - '0';
            sum2 += digit * (11 - i);
        }

        int dv2 = (sum2 * 10) % 11;
        if (dv2 == 10) dv2 = 0;

        if (dv2 != (cpfNumber[10] - '0'))
            return false;

        return true;
    }
}
