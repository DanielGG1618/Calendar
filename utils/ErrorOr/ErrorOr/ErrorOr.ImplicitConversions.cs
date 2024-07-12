namespace ErrorOr.ErrorOr;

public readonly partial record struct ErrorOr<TValue>
{
    public static implicit operator ErrorOr<TValue>(TValue value) => new(value);
    public static implicit operator ErrorOr<TValue>(Error error) => new([error]);
    public static implicit operator ErrorOr<TValue>(Error[] errors) => new(errors);
    public static implicit operator ErrorOr<TValue>(List<Error> errors) => new(errors);
    public static implicit operator ErrorOr<TValue>(ImmutableList<Error> errors) => new(errors);
}