namespace Domain.EventAggregate.Errors;

public static class CalendarEventErrors
{
    public static ErrorDetails InvalidTitle =>
        ErrorDetails.Validation(
            code: nameof(InvalidTitle),
            description: "title must not be empty"
        );
}