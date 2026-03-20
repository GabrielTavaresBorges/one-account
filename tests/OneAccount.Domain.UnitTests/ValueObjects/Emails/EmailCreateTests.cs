using FluentAssertions;
using OneAccount.Domain.ValueObjects.Emails;

namespace OneAccount.Domain.UnitTests.ValueObjects.Emails;

public class EmailCreateTests
{
    // IsFailure

    [Fact]
    public void Create_ShouldFail_WhenEmailIsNull()
    {
        // Arrange
        string emailAddress = null!;

        // Act
        var result = Email.Create(emailAddress);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Identifier.Should().Be("EMAIL_NULL_EMPTY");
        result.Error.Message.Should().Be("Email is required. " +
                                         "Please provide an address in the format 'example@domain.com'.");
    }

    [Fact]
    public void Create_ShouldFail_WhenEmailContainsWhiteSpace()
    {
        // Arrange
        string emailAddress = " ";

        // Act
        var result = Email.Create(emailAddress);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Identifier.Should().Be("EMAIL_NULL_EMPTY");
        result.Error.Message.Should().Be("Email is required. " +
                                         "Please provide an address in the format 'example@domain.com'.");
    }

    [Fact]
    public void Create_ShouldFail_WhenEmailIsTooLong()
    {
        // Arrange
        string emailAddress = new string('a', 255) + "@example.com";

        // Act
        var result = Email.Create(emailAddress);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Identifier.Should().Be("EMAIL_TOO_LONG");
        result.Error.Message.Should().Be(
            $"Email is too long. " +
            $"Current length: {emailAddress.Length} characters. " +
            $"Maximum allowed length: 254 characters.");
    }

    [Fact]
    public void Create_ShouldFail_WhenEmailFormatIsInvalid()
    {
        // Arrange
        string emailAddress = "invalid-email";

        // Act
        var result = Email.Create(emailAddress);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Identifier.Should().Be("EMAIL_INVALID_FORMAT");
        result.Error.Message.Should().Be(
            $"Email format is invalid. " +
            $"Expected format: 'example@domain.com'. " +
            $"Received value: '{emailAddress}'.");
    }   

    // IsSuccess

    [Fact]
    public void Create_ShouldSucceed_WhenEmailIsValid()
    {
        // Arrange
        string emailAddress = "tests@oneaccount.com";

        // Act
        var result = Email.Create(emailAddress);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.Should().NotBeNull();
        result.Value.EmailAddress.Should().Be(emailAddress);
    }

    [Fact]
    public void Create_ShouldSucceed_WhenEmailHasLeadingOrTrailingSpaces()
    {
        // Arrange
        string emailAddress = "  tests@oneaccount.com  ";

        // Act
        var result = Email.Create(emailAddress);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.EmailAddress.Should().Be("tests@oneaccount.com");
    }
}
