namespace Functional.Tests.ErrorOrMonad;

public class ErrorsCollectionTests
{
    [Fact]
    public void ShouldImplicitlyConvertErrorToErrorOr()
    {
        //Arrange
        var errors = new ErrorsCollection([ErrorDetails.Failure(), ErrorDetails.Forbidden()]);

        //Act
        ErrorOr<string> errorOr = errors;

        //Assert
        errorOr.Errors.Should().BeEquivalentTo(errors);
    }
}