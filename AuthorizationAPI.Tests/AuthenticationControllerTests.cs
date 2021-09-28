using AuthorizationAPI.Controllers;
using AuthorizationAPI.DTO;
using AuthorizationAPI.Models;
using AuthorizationAPI.Repository;
using AuthorizationAPI.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Net;

namespace AuthorizationAPI.Tests
{
    public class AuthenticationControllerTests
    {
        private Mock<IUserRepository> _userRepositoryStub;
        private Mock<ITokenService> _tokenServiceStub;
        [SetUp]
        public void Setup()
        {
            _userRepositoryStub = new Mock<IUserRepository>();
            _tokenServiceStub = new Mock<ITokenService>();
        }

        [Test]
        public void Login_WhenLoginSuccessful_ReturnsOkResult()
        {
            var user = new User { Id = 1, Username = "agent", Password = "agent"};
            _userRepositoryStub.Setup(repo => repo.GetUser(user.Username)).Returns(user);
            var controller = new AuthenticationController(_userRepositoryStub.Object, _tokenServiceStub.Object);
            var response = controller.Login(new UserDto { Username = "agent", Password = "agent" });
            response.Should().BeOfType<OkObjectResult>();
            (response as OkObjectResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Test]
        public void Login_WhenLoginUnSuccessful_ReturnsUnauthorizedResult()
        {
            var user = new User { Id = 1, Username = "agent", Password = "agent" };
            _userRepositoryStub.Setup(repo => repo.GetUser(user.Username)).Returns(user);
            var controller = new AuthenticationController(_userRepositoryStub.Object, _tokenServiceStub.Object);
            var response = controller.Login(new UserDto { Username = "agent", Password = "blahblah" });
            response.Should().BeOfType<UnauthorizedResult>();
            (response as UnauthorizedResult).StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        }
        [Test]
        public void Login_WhenUserDoesNotExist_ReturnsUnauthorizedResult()
        {
            var user = new User { Id = 1, Username = "admin", Password = "pass"};
            _userRepositoryStub.Setup(repo => repo.GetUser(user.Username)).Returns((User)null);
            var controller = new AuthenticationController(_userRepositoryStub.Object, _tokenServiceStub.Object);
            var response = controller.Login(new UserDto { Username = "admin", Password = "pass" });
            response.Should().BeOfType<UnauthorizedResult>();
            (response as UnauthorizedResult).StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
        }
    }
}
