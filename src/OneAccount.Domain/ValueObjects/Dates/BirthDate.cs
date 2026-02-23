using OneAccount.Domain.Abstraction.Exceptions;

namespace OneAccount.Domain.ValueObjects.Dates;

public sealed record BirthDate
{
    private const int MaxAgeYears = 150;

    public DateOnly Value { get; }

    private BirthDate(DateOnly value)
    {
        Value = value;
    }

    public static BirthDate Create(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (date > today)
            throw new DomainException(
                "Birth date cannot be in the future.",
                "BIRTH_DATE_IN_FUTURE");

        if (date < today.AddYears(-MaxAgeYears))
            throw new DomainException(
                $"Birth date cannot be more than {MaxAgeYears} years ago.",
                "BIRTH_DATE_TOO_OLD");

        var birthDate = new BirthDate(date);

        return birthDate;
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