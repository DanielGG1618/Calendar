namespace Functional.ErrorOrModad;

public readonly partial record struct ErrorOr<TValue>
{
    public void Switch(Action<TValue> onValue, Action<IList<ErrorDetails>> onError)
    {
        if (_value is not null)
            onValue(_value);
        else
            onError(_errors);
    }

    public Task Switch(Func<TValue, Task> onValue, Func<IList<ErrorDetails>, Task> onError) => 
        _value is not null 
            ? onValue(_value) 
            : onError(_errors);

    public void SwitchFirst(Action<TValue> onValue, Action<ErrorDetails> onError)
    {
        if (_value is not null)
            onValue(_value);
        else
            onError(_errors[0]);
    }

    public Task SwitchFirst(Func<TValue, Task> onValue, Func<ErrorDetails, Task> onError) =>
        _value is not null
            ? onValue(_value)
            : onError(_errors[0]);
}