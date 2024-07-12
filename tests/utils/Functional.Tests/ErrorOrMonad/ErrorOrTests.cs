using Functional.ErrorOrMonad;

namespace Functional.Tests.ErrorOrMonad;

public class ErrorOrTests
{
    [Fact]
    public void ShouldBeError_WhenCreatedWithErrorDetails()
    {
        //Arrange
        var error = ErrorDetails.Validation();

        //Act
        ErrorOr<string> errorOr = error;

        //Assert
        errorOr.IsError.Should().BeTrue();
        errorOr.Errors.Should().Contain(error);
    }
    
    [Fact]
    public void ShouldBeError_WhenCreatedWithMultipleErrorDetails()
    {
        //Arrange
        var errors = new List<ErrorDetails>
        {
            ErrorDetails.Validation(),
            ErrorDetails.Validation()
        };

        //Act
        ErrorOr<string> errorOr = errors;

        //Assert
        errorOr.IsError.Should().BeTrue();
        errorOr.Errors.Should().BeEquivalentTo(errors);
    }
    
    [Fact]
    public void ShouldBeValue_WhenCreatedWithValue()
    {
        //Arrange
        const string value = "value";

        //Act
        ErrorOr<string> errorOr = value;

        //Assert
        errorOr.IsValue.Should().BeTrue();
        errorOr.Value.Should().Be(value);
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationException_WhenValueIsAccessedOnAnError()
    {
        //Arrange
        var error = ErrorDetails.Validation();
        ErrorOr<string> errorOr = error;

        //Act
        var accessValue = () => _ = errorOr.Value;

        //Assert
        accessValue.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void ShouldThrowInvalidOperationException_WhenErrorsAreAccessedOnAValue()
    {
        //Arrange
        const string value = "value";
        ErrorOr<string> errorOr = value;

        //Act
        var accessErrors = () => _ = errorOr.Errors;

        //Assert
        accessErrors.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void ShouldThrowArgumentException_WhenCreatedWithEmptyErrorsList()
    {
        //Act
        var createErrorOr = () => _ = (ErrorOr<string>)Array.Empty<ErrorDetails>();

        //Assert
        createErrorOr.Should().ThrowExactly<ArgumentException>();
    }
    
    [Fact]
    public void ShouldMatchValue_WhenMatchedWithValue()
    {
        //Arrange
        const string value = "value";
        ErrorOr<string> errorOr = value;

        //Act
        var result = errorOr.Match(
            onValue: v => v,
            onError: _ => "error"
        );

        //Assert
        result.Should().Be(value);
    }
    
    [Fact]
    public void ShouldMatchError_WhenMatchedWithError()
    {
        //Arrange
        var error = ErrorDetails.Validation();
        ErrorOr<string> errorOr = error;

        //Act
        var result = errorOr.Match(
            onValue: v => v,
            onError: _ => "error"
        );

        //Assert
        result.Should().Be("error");
    }
    
    [Fact]
    public void ShouldMatchFirstError_WhenMatchedWithError()
    {
        //Arrange
        List<ErrorDetails> errors =
        [
            ErrorDetails.Validation(),
            ErrorDetails.Validation()
        ];
        ErrorOr<string> errorOr = errors;

        //Act
        var result = errorOr.MatchFirst(
            onValue: v => v,
            onError: e => e.Code
        );

        //Assert
        result.Should().Be(errors[0].Code);
    }
    
    [Fact]
    public void ShouldSwitchValue_WhenSwitchedWithValue()
    {
        //Arrange
        const string value = "value";
        ErrorOr<string> errorOr = value;

        //Act
        string result = null!;
        errorOr.Switch(
            onValue: v => result = v,
            onError: _ => result = "error"
        );

        //Assert
        result.Should().Be(value);
    }
    
    [Fact]
    public void ShouldSwitchError_WhenSwitchedWithError()
    {
        //Arrange
        var error = ErrorDetails.Validation();
        ErrorOr<string> errorOr = error;

        //Act
        string result = null!;
        errorOr.Switch(
            onValue: v => result = v,
            onError: _ => result = "error"
        );

        //Assert
        result.Should().Be("error");
    }
    
    [Fact]
    public void ShouldSwitchFirstError_WhenSwitchedWithError()
    {
        //Arrange
        List<ErrorDetails> errors =
        [
            ErrorDetails.Validation(),
            ErrorDetails.Validation()
        ];
        ErrorOr<string> errorOr = errors;

        //Act
        string result = null!;
        errorOr.SwitchFirst(
            onValue: v => result = v,
            onError: e => result = e.Code
        );

        //Assert
        result.Should().Be(errors[0].Code);
    }
}