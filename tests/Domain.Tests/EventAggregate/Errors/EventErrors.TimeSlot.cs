using ErrorOr.Errors;

namespace Domain.Tests.EventAggregate.Errors;

public static partial class EventErrors
{
    public static class TimeSlot
    {
        public static Error InvalidTimeRange =>
            Error.Validation(
                code: nameof(InvalidTimeRange),
                description: "from date must be earlier than the to date"
            );
    }
}