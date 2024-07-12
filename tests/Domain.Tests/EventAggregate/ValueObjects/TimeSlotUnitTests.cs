using Domain.EventAggregate.Errors;
using Domain.EventAggregate.Exceptions;
using Domain.EventAggregate.ValueObjects;

namespace Domain.Tests.EventAggregate.ValueObjects;

public class TimeSlotUnitTests
{
    [Fact]
    public void ShouldCreateTimeSlot_WhenFromIsBeforeTo()
    {
        //Arrange
        var from = DateTime.Now;
        var to = from.AddHours(1);

        //Act
        var timeSlot = TimeSlot.Create(from, to);
        
        //Assert
        timeSlot.From.Should().Be(from);
        timeSlot.To.Should().Be(to);
    }
    
    [Fact]
    public void ShouldThrowInvalidTimeRangeException_WhenCreateWithFromEqualsToTo()
    {
        //Arrange
        var fromAndTo = DateTime.Now;

        //Act
        var createTimeSlot = () => TimeSlot.Create(fromAndTo, fromAndTo);
        
        //Assert
        createTimeSlot.Should().ThrowExactly<TimeSlotExceptions.InvalidTimeRangeException>();
    }
    
    [Fact]
    public void ShouldThrowInvalidTimeRangeException_WhenCreateWithFromAfterTo()
    {
        //Arrange
        var from = DateTime.Now;
        var to = from.AddHours(-1);

        //Act
        var createTimeSlot = () => TimeSlot.Create(from, to);
        
        //Assert
        createTimeSlot.Should().ThrowExactly<TimeSlotExceptions.InvalidTimeRangeException>();
    }
    
    [Fact]
    public void ShouldReturnInvalidTimeRangeError_WhenSafeCreateWithFromEqualsToTo()
    {
        //Arrange
        var fromAndTo = DateTime.Now;

        //Act
        var errorOrTimeSlot = TimeSlot.SafeCreate(fromAndTo, fromAndTo);
        
        //Assert
        errorOrTimeSlot.IsError.Should().BeTrue();
        errorOrTimeSlot.Errors.Should().Contain(error => error.Code == TimeSlotErrors.InvalidTimeRange.Code);
    }
    
    [Fact]
    public void ShouldReturnInvalidTimeRangeError_WhenSafeCreateWithFromAfterTo()
    {
        //Arrange
        var from = DateTime.Now;
        var to = from.AddHours(-1);

        //Act
        var errorOrTimeSlot = TimeSlot.SafeCreate(from, to);
        
        //Assert
        errorOrTimeSlot.IsError.Should().BeTrue();
        errorOrTimeSlot.Errors.Should().Contain(error => error.Code == TimeSlotErrors.InvalidTimeRange.Code);
    }
}