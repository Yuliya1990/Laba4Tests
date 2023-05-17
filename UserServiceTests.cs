using NUnit.Framework;
using Moq;
using EasylifeAPI.Services;
using EasylifeAPI;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace EasyLifeAPI.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        Mock<IUserService> mockUserService;
        List<User> expectedUsers;
        [SetUp]
        public void Setup()
        {
            mockUserService = new Mock<IUserService>();
            expectedUsers = new List<User>
            {
                new User { Userid = 1, Email="email@exist.com"},
                new User { Userid = 2, PhoneNumber="00000000"},
                new User { Userid = 2, NIE="A0000000B"},
            };
        }
        [Test]
        public async Task RegisterUserTest()
        {
            // Arrange
            UserRegister new_user = new UserRegister
            {
                FirstName = "Yuliia",
                LastName = "Solomakha",
                Email = "solomakha.yu@knu.ua",
                NIE = "Y1234567H",
                PhoneNumb = "+345730267",
                Address = "Some address",
                Role = "Client",
                Password = "12345"
            };

            mockUserService.Setup(m => m.GetByEmail(new_user.Email)).Returns<User>(null);
            mockUserService.Setup(m => m.GetByPhone(new_user.PhoneNumb)).Returns<User>(null);
            mockUserService.Setup(m => m.GetByNIE(new_user.NIE)).Returns<User>(null);

            AuthService authService = new AuthService(mockUserService.Object);
            // Act
            authService.Register(new_user);
            // Assert
            mockUserService.Verify(m => m.GetByEmail(new_user.Email), Times.Once);
            mockUserService.Verify(m => m.GetByPhone(new_user.PhoneNumb), Times.Once);
            mockUserService.Verify(m => m.GetByNIE(new_user.NIE), Times.Once);
            mockUserService.Verify(m => m.AddClient(It.Is<Client>(cl => cl.Email==new_user.Email)));
        }

        [Test]
        public async Task RegisterEmailExistTest()
        {
            // Arrange
            UserRegister new_user = new UserRegister
            {
                FirstName = "Yuliia",
                LastName = "Solomakha",
                Email = "solomakha.yu@knu.ua",
                NIE = "Y1234567H",
                PhoneNumb = "+345730267",
                Address = "Some address",
                Role = "Client",
                Password = "12345"
            };
            mockUserService.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(new User { Email = new_user.Email });
            mockUserService.Setup(m => m.GetByPhone(It.IsAny<string>())).Returns<User>(null);
            mockUserService.Setup(m => m.GetByNIE(It.IsAny<string>())).Returns<User>(null);
            AuthService authService = new AuthService(mockUserService.Object);
            Assert.Throws<UserAlreadyExistsException>(() =>  authService.Register(new_user));
        }

        [Test]
        public async Task MockThrowsDBUpdateEception()
        {
            UserRegister new_user = new UserRegister
            {
                FirstName = "User",
                LastName = "User",
                Email = "user@gmail.com",
                NIE = "Y1234567H",
                PhoneNumb = "+345730267",
                Address = "Some address",
                Role = "Client",
                Password = "12345"
            };
            mockUserService.Setup(m => m.GetByEmail(new_user.Email)).Returns<User>(null);
            mockUserService.Setup(m => m.GetByPhone(new_user.PhoneNumb)).Returns<User>(null);
            mockUserService.Setup(m => m.GetByNIE(new_user.NIE)).Returns<User>(null);
            mockUserService.Setup(m => m.AddClient(It.Is<Client>(cl => cl.Email == new_user.Email))).Throws<DbUpdateException>();

            //Spy
            var partialMockObject = mockUserService.Object;
            AuthService authService = new AuthService(partialMockObject);
            authService.Register(new_user);
            //Перевірка, що була викликана реалізація mockUserService через partialMockObject
            mockUserService.Verify(m => m.AddClient(It.Is<Client>(cl => cl.Email == new_user.Email)), Times.Exactly(2));
        }
    }
}