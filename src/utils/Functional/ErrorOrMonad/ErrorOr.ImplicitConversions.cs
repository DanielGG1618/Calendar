namespace Functional.ErrorOrMonad;

public readonly partial record struct ErrorOr<TValue>
{
    public static implicit operator ErrorOr<TValue>(TValue value) => new(value);
    public static implicit operator ErrorOr<TValue>(ErrorDetails error) => new([error]);
    public static implicit operator ErrorOr<TValue>(ErrorDetails[] errors) => new(errors);
    public static implicit operator ErrorOr<TValue>(List<ErrorDetails> errors) => new(errors);
    public static implicit operator ErrorOr<TValue>(ImmutableList<ErrorDetails> errors) => new(errors);
}