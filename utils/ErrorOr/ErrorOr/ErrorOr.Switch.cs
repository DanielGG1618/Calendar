namespace ErrorOr.ErrorOr;

public readonly partial record struct ErrorOr<TValue>
{
    public void Switch(Action<TValue> onValue, Action<IList<Error>> onError)
    {
        if (_value is not null)
            onValue(_value);
        else
            onError(_errors);
    }

    public Task Switch(Func<TValue, Task> onValue, Func<IList<Error>, Task> onError) => 
        _value is not null 
            ? onValue(_value) 
            : onError(_errors);

    public void SwitchFirst(Action<TValue> onValue, Action<Error> onError)
    {
        if (_value is not null)
            onValue(_value);
        else
            onError(_errors.First());
    }

    public Task SwitchFirst(Func<TValue, Task> onValue, Func<Error, Task> onError) =>
        _value is not null
            ? onValue(_value)
            : onError(_errors.First());
}