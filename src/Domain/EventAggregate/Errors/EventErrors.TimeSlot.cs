using Functional.Errors;

namespace Domain.EventAggregate.Errors;

public static class TimeSlotErrors
{
    public static ErrorDetails InvalidTimeRange =>
        ErrorDetails.Validation(
            code: nameof(InvalidTimeRange),
            description: "from date must be earlier than the to date"
        );
}