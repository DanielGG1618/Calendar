namespace ErrorOr.ErrorOr;

public readonly partial record struct ErrorOr<TValue>
{
    public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<IList<Error>, TNextValue> onError) =>
        _value is not null
            ? onValue(_value)
            : onError(_errors);

    public Task<TNextValue> Match<TNextValue>(
        Func<TValue, Task<TNextValue>> onValue,
        Func<IList<Error>, Task<TNextValue>> onError
    ) => _value is not null
        ? onValue(_value)
        : onError(_errors);

    public TNextValue MatchFirst<TNextValue>(Func<TValue, TNextValue> onValue, Func<Error, TNextValue> onError) =>
        _value is not null
            ? onValue(_value)
            : onError(_errors.First());

    public Task<TNextValue> MatchFirst<TNextValue>(
        Func<TValue, Task<TNextValue>> onValue,
        Func<Error, Task<TNextValue>> onFirstError
    ) => _value is not null
        ? onValue(_value)
        : onFirstError(_errors.First());
}