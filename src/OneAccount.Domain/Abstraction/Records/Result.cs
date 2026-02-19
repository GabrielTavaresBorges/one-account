namespace OneAccount.Domain.Abstraction.Records;

public sealed record Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private readonly T? _value;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access Value when result is a failure.");

    public Error Error { get; }

    private Result(bool isSuccess, T? value, Error? error)
    {
        IsSuccess = isSuccess;

        if (isSuccess)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value), "Success result must have a value.");
            Error = Error.None;
        }
        else
        {
            _value = default;

            if (error is null)
                throw new ArgumentNullException(nameof(error), "Failure result must have an error.");

            if (error.IsNone)
                throw new ArgumentException("Failure result must have a non-empty error.", nameof(error));

            Error = error;
        }
    }

    public static Result<T> Success(T value) => new(true, value, null);

    public static Result<T> Failure(Error error) => new(false, default, error);

    public Result<K> Map<K>(Func<T, K> mapper) =>
        IsSuccess ? Result<K>.Success(mapper(Value)) : Result<K>.Failure(Error);

    public Result<K> Bind<K>(Func<T, Result<K>> binder) =>
        IsSuccess ? binder(Value) : Result<K>.Failure(Error);
}
