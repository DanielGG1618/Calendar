using Domain.Common.Models;
using Domain.EventAggregate.Errors;

namespace Domain.EventAggregate.ValueObjects;

public sealed class TimeSlot : ValueObjectBase
{
    public DateTime From { get; }
    public DateTime To { get; }

    public static TimeSlot Create(DateTime from, DateTime to) =>
        SafeCreate(from, to).Match(
            timeSlot => timeSlot,
            errors => throw new ArgumentException(errors.AggregatedDescription)
        );

    public static ErrorOr<TimeSlot> SafeCreate(DateTime from, DateTime to) =>
        from < to
            ? new TimeSlot(from, to)
            : TimeSlotErrors.InvalidTimeRange;

    private TimeSlot(DateTime from, DateTime to) => (From, To) = (from, to);
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return From;
        yield return To;
    }
}