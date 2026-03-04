using FluentAssertions;
using OneAccount.Domain.Entities.UserPhones;
using OneAccount.Domain.Entities.Users;
using OneAccount.Domain.Enumerators;
using OneAccount.Domain.ValueObjects.Dates;
using OneAccount.Domain.ValueObjects.Emails;
using OneAccount.Domain.ValueObjects.Names;
using OneAccount.Domain.ValueObjects.Security;

namespace OneAccount.Application.UnitTests.Entities.Users;

public class UserTests
{
    [Fact]
    public void Create_ShouldCreateUser_WithPendingEmailConfirmation_AndPrimaryPhone()
    {
        //Arrange

        var emailUser = "tests@oneaccount.com";
        var emailResult = Email.Create(emailUser);
        emailResult.IsSuccess.Should().BeTrue();
        var email = emailResult.Value;

        var passwordUser = "Tests@1234";
        var plainResult = PlainPassword.Create(passwordUser);
        plainResult.IsSuccess.Should().BeTrue();

        // Aqui você simula o hash (porque PasswordHash recebe string)        
        var fakeHash = $"HASH::{plainResult.Value.Password}";
        var hashResult = PasswordHash.Create(fakeHash);
        hashResult.IsSuccess.Should().BeTrue();
        var passwordHash = hashResult.Value;


        var userNameResult = UserName.Create("Name Tests");
        userNameResult.IsSuccess.Should().BeTrue();
        var userName = userNameResult.Value;


        var birthDateUser = new DateOnly(1986, 09, 03);
        var birthDateResult = BirthDate.Create(birthDateUser);
        birthDateResult.IsSuccess.Should().BeTrue();
        var birthDate = birthDateResult.Value;

        var gender = Gender.Male;

        var callingCode = "+55";
        var regionCode = "BR";
        var areaCode = "48";
        var phoneType = PhoneType.Mobile;
        var phoneNumber = "984477274";
        var e164 = "+5548984477274";
        var isPrimary = true;
        var phoneUserInital = UserPhone.Create(
            callingCode,
            regionCode,
            areaCode,
            phoneType,
            phoneNumber,
            e164,
            isPrimary);

        //Act

        var user = User.Create(email, passwordHash, userName, birthDate, gender, phoneUserInital);

        //Assert
        user.Should().NotBeNull();
        user.Status.Should().Be(AccountStatus.PendingEmailConfirmation);

        user.Email.Should().Be(email);
        user.UserName.Should().Be(userName);
        user.BirthDate.Should().Be(birthDate);
        user.Gender.Should().Be(Gender.Male);

        user.Phones.Should().HaveCount(1);
        user.Phones.Should().ContainSingle(p => p.IsPrimary);
        user.Phones.First().E164.Should().Be(e164);

        user.CreatedAt.Should().NotBe(default);
    }
}
