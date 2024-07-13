namespace Functional.ErrorOrMonad;

public static class ErrorsCollectionExtensions
{
    public static ErrorOr<TValue> WithValueIfEmpty<TValue>(this ErrorsCollection errors, TValue value) =>
        errors.IsEmpty
            ? value
            : errors;
}