using Domain.EventAggregate;
using Domain.EventAggregate.Errors;
using Domain.EventAggregate.ValueObjects;

namespace Domain.Tests.EventAggregate;

public class CalendarEventTests
{
    [Fact]
    public void ShouldCreateCalendarEvent_WhenValidData()
    {
        //Arrange
        var id = CalendarEventId.From(Guid.NewGuid());
        const string title = "title";
        const string description = "description";
        var timeSlot = TimeSlot.Create(DateTime.Now, DateTime.Now.AddHours(1));
        
        //Act
        var calendarEvent = CalendarEvent.Create(id, title, description, timeSlot);

        //Assert
        calendarEvent.Id.Should().Be(id);
        calendarEvent.Title.Should().Be(title);
        calendarEvent.Description.Should().Be(description);
        calendarEvent.TimeSlot.Should().Be(timeSlot);
    }
    
    [Fact]
    public void ShouldCreateNewCalendarEvent_WhenValidData()
    {
        //Arrange
        const string title = "title";
        const string description = "description";
        var timeSlot = TimeSlot.Create(DateTime.Now, DateTime.Now.AddHours(1));
        
        //Act
        var calendarEvent = CalendarEvent.CreateNew(title, description, timeSlot);

        //Assert
        calendarEvent.Title.Should().Be(title);
        calendarEvent.Description.Should().Be(description);
        calendarEvent.TimeSlot.Should().Be(timeSlot);
    }
    
    [Fact]
    public void ShouldThrowArgumentExceptionWithDescription_WhenCreateWithEmptyTitle()
    {
        //Arrange
        var id = CalendarEventId.From(Guid.NewGuid());
        var title1 = string.Empty;
        const string description = "description";
        var timeSlot = TimeSlot.Create(DateTime.Now, DateTime.Now.AddHours(1));
        
        //Act
        var createCalendarEvent = () => CalendarEvent.Create(id, title1, description, timeSlot);
        
        //Assert
        createCalendarEvent.Should().ThrowExactly<ArgumentException>()
            .WithMessage(CalendarEventErrors.InvalidTitle.Description);
    }
    
    [Fact]
    public void ShouldThrowArgumentExceptionWithDescription_WhenCreateNewWithEmptyTitle()
    {
        //Arrange
        var title = string.Empty;
        const string description = "description";
        var timeSlot = TimeSlot.Create(DateTime.Now, DateTime.Now.AddHours(1));
        
        //Act
        var createCalendarEvent = () => CalendarEvent.CreateNew(title, description, timeSlot);
        
        //Assert
        createCalendarEvent.Should().ThrowExactly<ArgumentException>()
            .WithMessage(CalendarEventErrors.InvalidTitle.Description);
    }
    
    [Fact]
    public void ShouldSafeCreateCalendarEvent_WhenValidData()
    {
        //Arrange
        var id = CalendarEventId.From(Guid.NewGuid());
        const string title = "title";
        const string description = "description";
        var timeSlot = TimeSlot.Create(DateTime.Now, DateTime.Now.AddHours(1));
        
        //Act
        var errorOrCalendarEvent = CalendarEvent.SafeCreate(id, title, description, timeSlot);

        //Assert
        errorOrCalendarEvent.Switch(
            calendarEvent =>
            {
                calendarEvent.Id.Should().Be(id);
                calendarEvent.Title.Should().Be(title);
                calendarEvent.Description.Should().Be(description);
                calendarEvent.TimeSlot.Should().Be(timeSlot);
            },
            errors => errors.Should().BeUnreachable()
        );
    }

    [Fact]
    public void ShouldSafeCreateNewCalendarEvent_WhenValidData()
    {
        //Arrange
        const string title = "title";
        const string description = "description";
        var timeSlot = TimeSlot.Create(DateTime.Now, DateTime.Now.AddHours(1));
        
        //Act
        var errorOrCalendarEvent = CalendarEvent.SafeCreateNew(title, description, timeSlot);
        
        //Assert
        errorOrCalendarEvent.Switch(
            calendarEvent =>
            {
                calendarEvent.Title.Should().Be(title);
                calendarEvent.Description.Should().Be(description);
                calendarEvent.TimeSlot.Should().Be(timeSlot);
            },
            errors => errors.Should().BeUnreachable()
        );
    }
    
    [Fact]
    public void ShouldReturnInvalidTitleError_WhenSafeCreateWithEmptyTitle()
    {
        //Arrange
        var id = CalendarEventId.From(Guid.NewGuid());
        var emptyTitle = string.Empty;
        const string description = "description";
        var timeSlot = TimeSlot.Create(DateTime.Now, DateTime.Now.AddHours(1));
        
        //Act
        var errorOrCalendarEvent = CalendarEvent.SafeCreate(id, emptyTitle, description, timeSlot);
        
        //Assert
        errorOrCalendarEvent.Switch(
            calendarEvent => calendarEvent.Should().BeUnreachable(),
            errors => errors.Should().Contain(CalendarEventErrors.InvalidTitle)
        );
    }
    
    [Fact]
    public void ShouldReturnInvalidTitleError_WhenSafeCreateNewWithEmptyTitle()
    {
        //Arrange
        var emptyTitle = string.Empty;
        const string description = "description";
        var timeSlot = TimeSlot.Create(DateTime.Now, DateTime.Now.AddHours(1));
        
        //Act
        var errorOrCalendarEvent = CalendarEvent.SafeCreateNew(emptyTitle, description, timeSlot);
        
        //Assert
        errorOrCalendarEvent.Switch(
            calendarEvent => calendarEvent.Should().BeUnreachable(),
            errors => errors.Should().Contain(CalendarEventErrors.InvalidTitle)
        );
    }
}