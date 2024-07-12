namespace Functional.ErrorOrMonad;

public readonly partial record struct ErrorOr<TValue>
{
    public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<IList<ErrorDetails>, TNextValue> onError) =>
        _value is not null
            ? onValue(_value)
            : onError(_errors);

    public Task<TNextValue> Match<TNextValue>(
        Func<TValue, Task<TNextValue>> onValue,
        Func<IList<ErrorDetails>, Task<TNextValue>> onError
    ) => _value is not null
        ? onValue(_value)
        : onError(_errors);

    public TNextValue MatchFirst<TNextValue>(Func<TValue, TNextValue> onValue, Func<ErrorDetails, TNextValue> onError) =>
        _value is not null
            ? onValue(_value)
            : onError(_errors[0]);

    public Task<TNextValue> MatchFirst<TNextValue>(
        Func<TValue, Task<TNextValue>> onValue,
        Func<ErrorDetails, Task<TNextValue>> onFirstError
    ) => _value is not null
        ? onValue(_value)
        : onFirstError(_errors[0]);
}