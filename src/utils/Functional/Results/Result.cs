namespace Functional.Results;

public static class Result
{
    public static Success Success { get; } = new();
    public static Updated Updated { get; } = new();
    public static Deleted Deleted { get; } = new();
}

public record Success
{
    internal Success()
    {
    }
}

public record Deleted
{
    internal Deleted()
    {
    }
}

public record Updated
{
    internal Updated()
    {
    }
}