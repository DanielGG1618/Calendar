namespace FluentAssertions.Extensions;

public static class ReferenceTypeAssertionsExtensions
{
    public static void BeUnreachable<TSubject, TAssertions>(
        this ReferenceTypeAssertions<TSubject, TAssertions> assertions,
        string because = "", params object[] becauseArgs)
        where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions> =>
        Execute.Assertion
            .ForCondition(false)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context} to be unreachable{reason}.", assertions.Subject);
}