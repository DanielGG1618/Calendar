using System.Collections;

namespace Functional.ErrorOrMonad;

public sealed class ErrorsCollection(IList<ErrorDetails> errors)
    : IEnumerable<ErrorDetails>
{
    public static ErrorsCollection Empty => new([]);

    private readonly ImmutableList<ErrorDetails> _errors = [.. errors];

    public ErrorDetails First => _errors[0];
    public bool IsEmpty => _errors.Count == 0;

    public string AggregatedDescription =>
        _errors
            .Select(e => e.Description)
            .Aggregate((d1, d2) => $"{d1}; {d2}");

    public ErrorsCollection With(ErrorDetails error) =>
        new(_errors.Add(error));

    public ErrorsCollection With(ErrorsCollection errors) =>
        new(_errors.AddRange(errors._errors));

    public ErrorsCollection With(IList<ErrorDetails> errors) =>
        new(_errors.AddRange(errors));

    public ErrorsCollection WithIf(bool condition, ErrorDetails error) =>
        condition
            ? With(error)
            : this;

    public ErrorsCollection WithIf(bool condition, ErrorsCollection errors) =>
        condition
            ? With(errors)
            : this;

    public ErrorsCollection WithIf(bool condition, IList<ErrorDetails> errors) =>
        condition
            ? With(errors)
            : this;

    public ErrorsCollection(ErrorDetails error) : this([error]) { }

    public IEnumerator<ErrorDetails> GetEnumerator() =>
        _errors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_errors).GetEnumerator();
}