using Domain.Tests.EventAggregate.Errors;

namespace Domain.Tests.EventAggregate.Exceptions;

public static partial class EventExceptions
{
    public static class TimeSlot
    {
        public class InvalidTimeRangeException : Exception
        {
            public override string Message { get; } = EventErrors.TimeSlot.InvalidTimeRange.Description;
        }
    }
}