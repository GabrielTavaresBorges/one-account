using FluentAssertions;
using OneAccount.Application.UseCases.Users.Commands.Create;
using OneAccount.Domain.Enumerators;

namespace OneAccount.Application.UnitTests.UseCases.Users.Commands.Create;

public class CommandValidatorTests
{
    [Fact]
    public void Validate_WhenCommandIsValid_ShouldBeValid()
    {
        // Arrange
        var validator = new CommandValidator();
        var command = new Command(
            Email: "test@hotmail.com",
            Password: "Tests@1234",
            UserName: "Name Test",
            BirthDate: new DateOnly(1986, 09, 03),
            Gender: Gender.Male,
            CallingCode: "+55",
            RegionCode: "BR",
            AreaCode: "48",
            PhoneType: PhoneType.Mobile,
            PhoneNumber: "984477274",
            E164: "+5548984477274"
        );

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_Email_IsInvalid_ShouldReturnError()
    {
        // Arrange
        var validator = new CommandValidator();

        var emailInvalid = "invalid-email";

        var command = new Command(
            Email: emailInvalid,
            Password: "Tests@1234",
            UserName: "Name Test",
            BirthDate: new DateOnly(1990, 01, 01),
            Gender: Gender.Male,
            CallingCode: "+55",
            RegionCode: "BR",
            AreaCode: "48",
            PhoneType: PhoneType.Mobile,
            PhoneNumber: "987294620",
            E164: "+5511987294620"
        );

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
        result.Errors[0].PropertyName.Should().Be("Email");
    }
}