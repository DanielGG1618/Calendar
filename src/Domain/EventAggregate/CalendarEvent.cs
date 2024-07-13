using Domain.Common.Models;
using Domain.EventAggregate.Errors;
using Domain.EventAggregate.ValueObjects;

namespace Domain.EventAggregate;

public sealed class CalendarEvent : AggregateRoot<CalendarEventId>
{
    public string Title { get; private init; } = string.Empty;
    public string Description { get; private init; } = string.Empty;
    public TimeSlot TimeSlot { get; private init; } = null!;

    public static CalendarEvent CreateNew(
        string title,
        string description,
        TimeSlot timeSlot
    ) => SafeCreateNew(title, description, timeSlot).Match(
        calendarEvent => calendarEvent,
        errors => throw new ArgumentException(errors.AggregatedDescription)
    );
    
    public static CalendarEvent Create(
        CalendarEventId id,
        string title,
        string description,
        TimeSlot timeSlot
    ) => SafeCreate(id, title, description, timeSlot).Match(
        calendarEvent => calendarEvent,
        errors => throw new ArgumentException(errors.AggregatedDescription)
    );

    public static ErrorOr<CalendarEvent> SafeCreateNew(
        string title,
        string description,
        TimeSlot timeSlot
    ) => SafeCreate(CalendarEventId.Unique(), title, description, timeSlot);

    public static ErrorOr<CalendarEvent> SafeCreate(
        CalendarEventId id,
        string title,
        string description,
        TimeSlot timeSlot
    ) => ErrorsCollection.Empty
        .WithIf(string.IsNullOrEmpty(title), CalendarEventErrors.InvalidTitle)
        .WithValueIfEmpty(
            new CalendarEvent(id, title, description, timeSlot)
        );
    
    private CalendarEvent(CalendarEventId id, string title, string description, TimeSlot timeSlot) : base(id) =>
        (Title, Description, TimeSlot) = (title, description, timeSlot);
    
    // ReSharper disable once UnusedMember.Local
    private CalendarEvent() { }
}