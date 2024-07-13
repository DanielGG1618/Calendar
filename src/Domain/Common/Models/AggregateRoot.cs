namespace Domain.Common.Models;

public abstract class AggregateRoot<TId> : EntityBase<TId>
    where TId : notnull
{
    protected AggregateRoot(TId id) : base(id) {}
    protected AggregateRoot() {}
}