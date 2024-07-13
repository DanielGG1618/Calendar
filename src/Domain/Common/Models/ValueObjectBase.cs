namespace Domain.Common.Models;

public abstract class ValueObjectBase 
    : IEquatable<ValueObjectBase>
{
    public static bool operator ==(ValueObjectBase? left, ValueObjectBase? right) =>
        Equals(left, right);

    public static bool operator !=(ValueObjectBase? left, ValueObjectBase? right) =>
        !(left == right);

    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObjectBase? other) =>
        Equals((object?)other);

    public override bool Equals(object? obj) =>
        obj is not null 
        && obj.GetType() == GetType() 
        && obj is ValueObjectBase other 
        && GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());

    public override int GetHashCode() =>
        GetEqualityComponents()
            .Select(x => x is null ? 0 : x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
}