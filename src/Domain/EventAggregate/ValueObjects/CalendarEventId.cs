using Domain.Common.Models;

namespace Domain.EventAggregate.ValueObjects;

public sealed class CalendarEventId : ValueObjectBase, IId<CalendarEventId, Guid>
{
    public Guid Value { get; }
 
    public static CalendarEventId Unique() =>
        new(Guid.NewGuid());
    
    public static CalendarEventId From(Guid value) =>
        new(value);
    
    private CalendarEventId(Guid value) =>
        Value = value;
    
    public static implicit operator string(CalendarEventId id) =>
        id.Value.ToString();

    public static implicit operator CalendarEventId(string str) => 
        From(Guid.Parse(str));
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}