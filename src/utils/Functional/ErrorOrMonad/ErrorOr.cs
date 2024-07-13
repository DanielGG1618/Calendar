namespace Functional.ErrorOrMonad;

public readonly partial record struct ErrorOr<TValue>
{
    private readonly TValue? _value = default;
    private readonly ErrorsCollection _errors = ErrorsCollection.Empty;

    public bool IsValue => _value is not null;
    public bool IsError => !_errors.IsEmpty;
    
    public TValue Value => _value is not null
        ? _value
        : throw new InvalidOperationException("ErrorOr<TValue> does not contain a value");
    
    public ErrorsCollection Errors => IsError
        ? _errors
        : throw new InvalidOperationException("ErrorOr<TValue> does not contain errors");
    
    private ErrorOr(TValue value) => 
        _value = value;
    
    private ErrorOr(ErrorDetails error) =>
        _errors = new ErrorsCollection(error);

    private ErrorOr(IList<ErrorDetails> errors) =>
        _errors = errors.Any()
            ? new ErrorsCollection(errors)
            : throw new ArgumentException(
                "Creation of ErrorOr<TValue> with empty list of errors is impossible",
                nameof(errors)
            );

    private ErrorOr(ErrorsCollection errors) =>
        _errors = !errors.IsEmpty
            ? errors
            : throw new ArgumentException(
                "Creation of ErrorOr<TValue> with empty list of errors is impossible",
                nameof(errors)
            );
}