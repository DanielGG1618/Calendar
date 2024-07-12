namespace ErrorOr.Errors;

public readonly record struct Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }
    public Dictionary<string, object> Metadata { get; }

    private Error(string code, string description, ErrorType type, Dictionary<string, object> metadata) =>
        (Code, Description, Type, Metadata) = (code, description, type, metadata);

    public static Error Failure(
        string code = "General.Failure",
        string description = "A failure has occurred.",
        Dictionary<string, object>? metadata = null
    ) => new(code, description, ErrorType.Failure, metadata ?? []);

    public static Error Unexpected(
        string code = "General.Unexpected",
        string description = "An unexpected error has occurred.",
        Dictionary<string, object>? metadata = null
    ) => new(code, description, ErrorType.Unexpected, metadata ?? []);

    public static Error Validation(
        string code = "General.Validation",
        string description = "A validation error has occurred.",
        Dictionary<string, object>? metadata = null
    ) => new(code, description, ErrorType.Validation, metadata ?? []);

    public static Error Conflict(
        string code = "General.Conflict",
        string description = "A conflict error has occurred.",
        Dictionary<string, object>? metadata = null
    ) => new(code, description, ErrorType.Conflict, metadata ?? []);

    public static Error NotFound(
        string code = "General.NotFound",
        string description = "A 'Not Found' error has occurred.",
        Dictionary<string, object>? metadata = null
    ) => new(code, description, ErrorType.NotFound, metadata ?? []);

    public static Error Unauthorized(
        string code = "General.Unauthorized",
        string description = "An 'Unauthorized' error has occurred.",
        Dictionary<string, object>? metadata = null
    ) => new(code, description, ErrorType.Unauthorized, metadata ?? []);
    
    public static Error Forbidden(
        string code = "General.Forbidden",
        string description = "A 'Forbidden' error has occurred.",
        Dictionary<string, object>? metadata = null
    ) => new(code, description, ErrorType.Forbidden, metadata ?? []);

    public bool Equals(Error other) =>
        Type == other.Type 
        && Code == other.Code 
        && Description == other.Description 
        && CompareMetadata(Metadata, other.Metadata);

    public override int GetHashCode() =>
        HashCode.Combine(
            Code,
            Description,
            Type,
            Metadata.SelectMany(pair => new[] { pair.Key, pair.Value })
        );

    private static bool CompareMetadata(
        Dictionary<string, object> metadata,
        Dictionary<string, object> otherMetadata
    ) => ReferenceEquals(metadata, otherMetadata)
         || metadata.Count == otherMetadata.Count && metadata.All(pair =>
             otherMetadata.TryGetValue(pair.Key, out var otherValue) && pair.Value.Equals(otherValue)
         );
}
