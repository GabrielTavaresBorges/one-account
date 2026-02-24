using OneAccount.Domain.Abstraction.Records;

namespace OneAccount.Domain.ValueObjects.Dates;

public sealed record BirthDate
{
    public DateOnly Value { get; private init; }

    private BirthDate() { }

    private BirthDate(DateOnly birthDate)
    {
        Value = birthDate;
    }

    public static Result<BirthDate> Create(DateOnly date)
    {
        var validatedBirthDate = Validate(date);

        if (validatedBirthDate.IsFailure)
            return Result<BirthDate>.Failure(validatedBirthDate.Error);

        return Result<BirthDate>.Success(new BirthDate(validatedBirthDate.Value));
    }

    private static Result<DateOnly> Validate(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (date > today)
        {
            return Result<DateOnly>.Failure(new Error(
                Identifier: "BIRTH_DATE_IN_FUTURE",
                Message: "Birth date cannot be in the future.\n" +
                         $"Received: {date:yyyy-MM-dd}.\n" +
                         $"Today (UTC): {today:yyyy-MM-dd}."
            ));
        }

        const int MaxAgeYears = 150;

        var minAllowed = today.AddYears(-MaxAgeYears);

        if (date < minAllowed)
        {
            return Result<DateOnly>.Failure(new Error(
                Identifier: "BIRTH_DATE_TOO_OLD",
                Message: $"Birth date cannot be more than {MaxAgeYears} years ago.\n" +
                         $"Received: {date:yyyy-MM-dd}.\n" +
                         $"Minimum allowed (UTC): {minAllowed:yyyy-MM-dd}."
            ));
        }

        return Result<DateOnly>.Success(date);
    }

    public int GetAge()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - Value.Year;

        if (Value > today.AddYears(-age))
            age--;

        return age;
    }

    public bool IsAdult(int adultAge = 18)
        => GetAge() >= adultAge;

    public int GetDaysOfLife()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return today.DayNumber - Value.DayNumber;
    }
}