namespace Functional.ErrorOrMonad;

public readonly partial record struct ErrorOr<TValue>
{
    public ErrorOr<TValue> AppendErrorIf(bool condition, ErrorDetails error) =>
        condition ? _errors.With(error) : this;
}