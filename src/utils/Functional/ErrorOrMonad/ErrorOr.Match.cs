namespace Functional.ErrorOrMonad;

public readonly partial record struct ErrorOr<TValue>
{
    public TNextValue Match<TNextValue>(
        Func<TValue, TNextValue> onValue,
        Func<ErrorsCollection, TNextValue> onError
    ) => _value is not null
            ? onValue(_value)
            : onError(_errors);

    public Task<TNextValue> Match<TNextValue>(
        Func<TValue, Task<TNextValue>> onValue,
        Func<ErrorsCollection, Task<TNextValue>> onError
    ) => _value is not null
        ? onValue(_value)
        : onError(_errors);

    public TNextValue MatchFirst<TNextValue>(Func<TValue, TNextValue> onValue, Func<ErrorDetails, TNextValue> onError) =>
        _value is not null
            ? onValue(_value)
            : onError(_errors.First);

    public Task<TNextValue> MatchFirst<TNextValue>(
        Func<TValue, Task<TNextValue>> onValue,
        Func<ErrorDetails, Task<TNextValue>> onFirstError
    ) => _value is not null
        ? onValue(_value)
        : onFirstError(_errors.First);
}