using Domain.Tests.EventAggregate.Errors;
using Domain.Tests.EventAggregate.Exceptions;
using ErrorOr.ErrorOr;

namespace Domain.Tests.EventAggregate.ValueObjects;

public class TimeSlotUnitTests
{
    [Fact]
    public void ShouldThrowInvalidTimeRangeException_WhenCreateWithFromEqualsToTo()
    {
        //Arrange
        var fromAndTo = DateTime.Now;

        //Act
        var createTimeSlot = () => TimeSlot.Create(fromAndTo, fromAndTo);
        
        //Assert
        createTimeSlot.Should().ThrowExactly<EventExceptions.TimeSlot.InvalidTimeRangeException>();
    }
    
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
    public void ShouldReturnInvalidTimeRangeError_WhenSafeCreateWithFromEqualsToTo()
    {
        //Arrange
        var fromAndTo = DateTime.Now;

        //Act
        var errorOrTimeSlot = TimeSlot.SafeCreate(fromAndTo, fromAndTo);
        
        //Assert
        errorOrTimeSlot.IsError.Should().BeTrue();
        errorOrTimeSlot.Errors.Should().Contain(error => error.Code == EventErrors.TimeSlot.InvalidTimeRange.Code);
    }
}

public class TimeSlot
{
    public DateTime From { get; }
    public DateTime To { get; }

    public static TimeSlot Create(DateTime from, DateTime to) =>
        from < to ? new(from, to) 
            : throw new EventExceptions.TimeSlot.InvalidTimeRangeException();
    
    private TimeSlot(DateTime from, DateTime to) => (From, To) = (from, to);

    public static ErrorOr<TimeSlot> SafeCreate(DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}