using Adly.Application.Contracts.User;
using Adly.Application.Contracts.User.Models;
using Adly.Application.Features.User.Commands.Register;
using Adly.Application.Features.User.Queries.PasswordLogin;
using Adly.Application.Tests.Extensions;
using Bogus;
using Domain.Entities.User;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Xunit.Abstractions;

namespace Adly.Application.Tests.UserTests
{
    public class UserFeaturesTest(ITestOutputHelper testOutputHelper)
    {

        [Fact]
        public async Task Creating_New_User_Should_Be_Success()
        {
            //Arrange 
            var faker = new Faker();
            var password = faker.Random.String(10);

            var registerUserRequest = new RegisterUserCommand(faker.Person.FirstName,
                faker.Person.LastName,
                faker.Person.UserName,
                faker.Person.Email,
                faker.Person.Phone,
                password, password);

            var userManager = Substitute.For<IUserManager>();

            userManager.PasswordCreateAsync(Arg.Any<UserEntity>(), registerUserRequest.Password, CancellationToken.None).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var userRegisterCommandHandler = new RegisterUserCommandHandler(userManager);


            var userRegisterResult = await userRegisterCommandHandler.Handle(registerUserRequest, CancellationToken.None);

            userRegisterResult.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Creating_New_User_Email_Should_Be_Valid()
        {
            //Arrange 
            var faker = new Faker();
            var password = faker.Random.String(10);

            var registerUserRequest = new RegisterUserCommand(faker.Person.FirstName,
                faker.Person.LastName,
                faker.Person.UserName,
                string.Empty,
                faker.Person.Phone,
                password, password);

            var userManager = Substitute.For<IUserManager>();

            userManager.PasswordCreateAsync(Arg.Any<UserEntity>(), registerUserRequest.Password, CancellationToken.None).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var userRegisterCommandHandler = new RegisterUserCommandHandler(userManager);


            var userRegisterResult = await userRegisterCommandHandler.Handle(registerUserRequest, CancellationToken.None);

            userRegisterResult.IsSuccess.Should().BeFalse();

            testOutputHelper.WriteLineOperationResultErrors(userRegisterResult);
        }

        [Fact]
        public async Task Register_User_Password_And_Repeat_Password_Should_Be_Same()
        {
            //Arrange 
            var faker = new Faker();

            var registerUserRequest = new RegisterUserCommand(faker.Person.FirstName,
               faker.Person.LastName,
               faker.Person.UserName,
               string.Empty,
               faker.Person.Phone,
               Guid.NewGuid().ToString("N"), Guid.NewGuid().ToString("N"));

            var userManager = Substitute.For<IUserManager>();

            userManager.PasswordCreateAsync(Arg.Any<UserEntity>(), registerUserRequest.Password, CancellationToken.None).Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var userRegisterCommandHandler = new RegisterUserCommandHandler(userManager);


            var userRegisterResult = await userRegisterCommandHandler.Handle(registerUserRequest, CancellationToken.None);

            userRegisterResult.IsSuccess.Should().BeFalse();

            testOutputHelper.WriteLineOperationResultErrors(userRegisterResult);

        }

        [Fact]
        public async Task Password_Login_User_With_UserName_Should_Be_Success()
        {
            //Arrange
            var faker = new Faker();
            var loginQuery = new UserPasswordLoginQuery(faker.Person.UserName, Guid.NewGuid().ToString("N"));


            var userManager = Substitute.For<IUserManager>();

            var userEntity = new UserEntity(faker.Person.FirstName,
                faker.Person.LastName, faker.Person.UserName, faker.Person.Email);


            userManager.GetUserByUserNameAsync(loginQuery.userNameOrEmail, CancellationToken.None)
                .Returns(Task.FromResult<UserEntity?>(userEntity));

            userManager.ValidatePasswordAsync(userEntity, loginQuery.password, CancellationToken.None)
                .Returns(Task.FromResult<IdentityResult>(IdentityResult.Success));

            var jwtService = Substitute.For<IJwtService>();

            jwtService.GenerateTokenAsync(userEntity, CancellationToken.None)
                .Returns(Task.FromResult<JwtAccessTokenModel>(new JwtAccessTokenModel("new accessToken", 3000)));

            //Act 
            var userLogingQueryHandler = new UserPasswordLoginQueryHandler(userManager, jwtService);

            var loginResult = await userLogingQueryHandler.Handle(loginQuery, CancellationToken.None);


            //Assert 
            loginResult.Result.Should().NotBeNull();
            loginResult.IsSuccess.Should().BeTrue();

        }

        [Fact]

        public async Task Password_Login_User_With_UserName_And_Wrong_Should_Be_Failure()
        {
            //Arrange
            var faker = new Faker();
            var loginQuery = new UserPasswordLoginQuery(faker.Person.UserName, Guid.NewGuid().ToString("N"));


            var userManager = Substitute.For<IUserManager>();

            var userEntity = new UserEntity(faker.Person.FirstName,
                faker.Person.LastName, faker.Person.UserName, faker.Person.Email);


            userManager.GetUserByUserNameAsync(loginQuery.userNameOrEmail, CancellationToken.None)
                .Returns(Task.FromResult<UserEntity?>(userEntity));

            userManager.ValidatePasswordAsync(userEntity, loginQuery.password, CancellationToken.None)
                .Returns(Task.FromResult<IdentityResult>(IdentityResult.Failed()));

            var jwtService = Substitute.For<IJwtService>();

            jwtService.GenerateTokenAsync(userEntity, CancellationToken.None)
                .Returns(Task.FromResult<JwtAccessTokenModel>(new JwtAccessTokenModel("new accessToken", 3000)));

            //Act 
            var userLogingQueryHandler = new UserPasswordLoginQueryHandler(userManager, jwtService);

            var loginResult = await userLogingQueryHandler.Handle(loginQuery, CancellationToken.None);


            //Assert 
            loginResult.Result.Should().BeNull();
            loginResult.IsSuccess.Should().BeFalse();


            testOutputHelper.WriteLineOperationResultErrors(loginResult);

        }
    }
}
