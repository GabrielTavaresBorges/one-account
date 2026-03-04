using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OneAccount.Application.Services.Security.Interfaces;
using OneAccount.Application.UseCases.Users.Commands.Create;
using OneAccount.Domain.Abstraction.Interfaces;
using OneAccount.Domain.Entities.Users;
using OneAccount.Domain.Enumerators;
using OneAccount.Domain.Repositories.UsersRepository;

namespace OneAccount.Application.UnitTests.UseCases.Users.Commands.Create;

public class HandlerTests
{
    [Fact]
    public async Task Handle_WhenCommandIsValid_ShouldCreateUser_AndCommit_AndReturnSuccess()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
        var unitOfWork = new Mock<IUnityOfWork>(MockBehavior.Strict);
        var logger = new Mock<ILogger<Handler>>();
        var passwordHasher = new Mock<IPasswordHasher>(MockBehavior.Strict);

        var command = new Command(
            Email: "test@hotmail.com",
            Password: "Tests@1234",
            UserName: "Name Test",
            BirthDate: new DateOnly(1990, 01, 01),
            Gender: Gender.Male,
            CallingCode: "+55",
            RegionCode: "BR",
            AreaCode: "48",
            PhoneType: PhoneType.Mobile,
            PhoneNumber: "987294620",
            E164: "+5548987294620"
        );

        passwordHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("HASH::OK");

        userRepository
            .Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        unitOfWork
            .Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

        var handler = new Handler(
            usersRepository: userRepository.Object,
            unitOfWork: unitOfWork.Object,
            logger: logger.Object,
            passwordHasher: passwordHasher.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Message.Should().Be("User created successfully!");
        result.Value.UserName.Should().Be(command.UserName);
        result.Value.Id.Should().NotBe(Guid.Empty);

        passwordHasher.Verify(x => x.Hash(It.Is<string>(p => p == command.Password)), Times.Once);
        userRepository.Verify(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenEmailIsInvalid_ShouldReturnFailure_AndNotCallRepository()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
        var unitOfWork = new Mock<IUnityOfWork>(MockBehavior.Strict);
        var logger = new Mock<ILogger<Handler>>();
        var passwordHasher = new Mock<IPasswordHasher>(MockBehavior.Strict);

        var command = new Command(
            Email: "invalid-email",
            Password: "Tests@1234",
            UserName: "Name Test",
            BirthDate: new DateOnly(1990, 01, 01),
            Gender: Gender.Male,
            CallingCode: "+55",
            RegionCode: "BR",
            AreaCode: "48",
            PhoneType: PhoneType.Mobile,
            PhoneNumber: "987294620",
            E164: "+5548987294620"
        );

        var handler = new Handler(
            usersRepository: userRepository.Object,
            unitOfWork: unitOfWork.Object,
            logger: logger.Object,
            passwordHasher: passwordHasher.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        // se você tiver Identifier/Message no Error:
        result.Error.Identifier.Should().NotBeNullOrWhiteSpace();

        passwordHasher.Verify(x => x.Hash(It.IsAny<string>()), Times.Never);
        userRepository.Verify(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenDomainExceptionOccurs_ShouldReturnFailure()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
        var unitOfWork = new Mock<IUnityOfWork>(MockBehavior.Strict);
        var logger = new Mock<ILogger<Handler>>();
        var passwordHasher = new Mock<IPasswordHasher>(MockBehavior.Strict);

        // Força DomainException no UserPhone.Create: callingCode inválido (não começa com "+")
        var command = new Command(
            Email: "test@hotmail.com",
            Password: "Tests@1234",
            UserName: "Name Test",
            BirthDate: new DateOnly(1990, 01, 01),
            Gender: Gender.Male,
            CallingCode: "55", // inválido => DomainException
            RegionCode: "BR",
            AreaCode: "48",
            PhoneType: PhoneType.Mobile,
            PhoneNumber: "987294620",
            E164: "+5548987294620"
        );

        passwordHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("HASH::OK");

        var handler = new Handler(
            usersRepository: userRepository.Object,
            unitOfWork: unitOfWork.Object,
            logger: logger.Object,
            passwordHasher: passwordHasher.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Identifier.Should().Be("PHONE_CALLING_CODE_INVALID");

        userRepository.Verify(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenUnexpectedExceptionOccurs_ShouldReturnUnexpectedError()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
        var unitOfWork = new Mock<IUnityOfWork>(MockBehavior.Strict);
        var logger = new Mock<ILogger<Handler>>();
        var passwordHasher = new Mock<IPasswordHasher>(MockBehavior.Strict);

        var command = new Command(
            Email: "test@hotmail.com",
            Password: "Tests@1234",
            UserName: "Name Test",
            BirthDate: new DateOnly(1990, 01, 01),
            Gender: Gender.Male,
            CallingCode: "+55",
            RegionCode: "BR",
            AreaCode: "48",
            PhoneType: PhoneType.Mobile,
            PhoneNumber: "987294620",
            E164: "+5548987294620"
        );

        passwordHasher
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("HASH::OK");

        userRepository
            .Setup(x => x.CreateUserAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("db down"));

        // unitOfWork não deve ser chamado se CreateUserAsync explodiu
        var handler = new Handler(
            usersRepository: userRepository.Object,
            unitOfWork: unitOfWork.Object,
            logger: logger.Object,
            passwordHasher: passwordHasher.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Identifier.Should().Be("UNEXPECTED_ERROR");

        unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
    }
}