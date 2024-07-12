namespace Functional.ErrorOrMonad;

public readonly partial record struct ErrorOr<TValue>
{
    private readonly TValue? _value = default;
    private readonly ImmutableList<ErrorDetails> _errors = [];

    public bool IsValue => _value is not null;
    public bool IsError => _errors.Count > 0;
    
    public TValue Value => _value is not null
        ? _value
        : throw new InvalidOperationException("ErrorOr<TValue> does not contain a value");
    
    public IEnumerable<ErrorDetails> Errors => IsError
        ? _errors
        : throw new InvalidOperationException("ErrorOr<TValue> does not contain errors");
    
    private ErrorOr(TValue value) => _value = value;

    private ErrorOr(IList<ErrorDetails> errors) =>
        _errors = errors.Any() ? errors.ToImmutableList()
            : throw new ArgumentException(
                "Creation of ErrorOr<TValue> with empty list of errors is impossible",
                nameof(errors)
            );
}