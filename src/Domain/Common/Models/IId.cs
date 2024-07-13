namespace Domain.Common.Models;

public interface IId<TSelf, TSource> 
    where TSelf : IId<TSelf, TSource>
    where TSource : notnull
{
    public TSource Value { get; }

    public static abstract TSelf Unique();
    public static abstract TSelf From(TSource value);
    
    public static abstract implicit operator string(TSelf id);
    public static abstract implicit operator TSelf(string str);
}