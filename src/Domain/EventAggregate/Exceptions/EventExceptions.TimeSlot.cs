using Domain.EventAggregate.Errors;

namespace Domain.EventAggregate.Exceptions;

public static class TimeSlotExceptions
{
    public class InvalidTimeRangeException : Exception
    {
        public override string Message { get; } = TimeSlotErrors.InvalidTimeRange.Description;
    }
}