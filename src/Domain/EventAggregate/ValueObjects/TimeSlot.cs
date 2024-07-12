using Domain.EventAggregate.Errors;
using Domain.EventAggregate.Exceptions;
using Functional.ErrorOrModad;

namespace Domain.EventAggregate.ValueObjects;

public class TimeSlot
{
    public DateTime From { get; }
    public DateTime To { get; }

    public static TimeSlot Create(DateTime from, DateTime to) =>
        from < to
            ? new TimeSlot(from, to)
            : throw new TimeSlotExceptions.InvalidTimeRangeException();

    public static ErrorOr<TimeSlot> SafeCreate(DateTime from, DateTime to) =>
        from < to
            ? new TimeSlot(from, to)
            : TimeSlotErrors.InvalidTimeRange;

    private TimeSlot(DateTime from, DateTime to) => (From, To) = (from, to);
}