using Aplication.Services;
using Domain.Interfaces;
using Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aplication.Tests
{
    
    public class UsertServiceTests
    {
        private readonly UserService _userService;
        
        private readonly Mock<IUserRepository> _mockedUserRepository = new Mock<IUserRepository>();

        public UsertServiceTests()
        {
            _userService = new UserService(_mockedUserRepository.Object);
        }

        [Fact]
        public void GetAllUsers_ReturnsList()
        {
            // Arrange
            _mockedUserRepository.Setup(a => a.GetAllUsers())
                .Returns(Users);

            //Act
            var list = _userService.GetAllUsers();

            //Assert

            Assert.Equal(list, Users);
        }

        [Fact]

        public void GetAllUsers_ReturnsEmptyList()
        {
            // Arrange
            _mockedUserRepository.Setup(a => a.GetAllUsers())
                .Returns(new List<User>());

            //Act
            var list = _userService.GetAllUsers();

            //Assert

            Assert.True(list.Count == 0);
        }
        [Theory]

        [InlineData(0, 3.4)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 0.2)]

        public void GetUserBalance_ReturnsCorrectValue(int user_id, decimal expected_balance)
        {
            // Arrange
            _mockedUserRepository.Setup(a => a.GetUserById(user_id))
                .Returns(Users.FirstOrDefault(user => user_id == user.Id));
            //Act
            var actual_balance = _userService.GetUserBalance(user_id);

            //Assert

            Assert.Equal(expected_balance, actual_balance);
        }

        [Theory]
        [InlineData(0,"a")]
        [InlineData(1,"b")]

        public void ChangeUserData_ValidUserId(int userId, string new_password)
        {
            // Arrange
            _mockedUserRepository.Setup(a => a.UpdateUser(Users[userId].Id, Users[userId].Email, Users[userId].Balance, new_password))
                .Returns(() =>
                {
                    Users[userId].Password = new_password;
                    return true;
                });
            //Act
            string actual_balance = _userService.ChangeUserData(userId, Users[userId].Email, Users[userId].Balance, new_password);

            //Assert

            Assert.True(Users[userId].Password == new_password);

            Assert.Equal("Password was updated successfuly", actual_balance);
        }
        [Fact]
        public void ChangeUserData_NotValidUserId()
        {
            const int userId = int.MaxValue;

            // Arrange
            _mockedUserRepository.Setup(a => a.UpdateUser(userId, "email", 0, "new_password"))
                .Returns(false);
            //Act
            var actual_balance = _userService.ChangeUserData(userId, "email", 0, "new_password");

            //Assert

            Assert.Equal("No user with such id", actual_balance);
        }
        [Theory]
        [InlineData("1@1", "1",true)]
        [InlineData("2@2","2",false)]
        public void Login_RegisteredUser(string email, string password, bool isAlreadyRegisterd)
        {
            // Arrange
            _mockedUserRepository.Setup(a => a.GetAllUsers())
                .Returns(Users);
            //Act
            var user = _userService.Login(email, password);

            //Assert
            if(!isAlreadyRegisterd) Assert.Null(user);
            else Assert.NotNull(user);
        }

        public List<User> Users { get; set; } = new List<User>()
        {
            new User {Id = 0, Email = "1@1", Password = "1", IsAdmin = true, Balance = 3.4M },
            new User {Id = 1, Email = "email0@test.com", Password = "pass0" , Balance = 0},
            new User {Id = 2, Email = "email1@test.com", Password = "pass1", Balance = 1 },
            new User {Id = 3, Email = "email2@test.com", Password = "pass2", Balance = 0.2M }
        };
        public List<Product> Products { get; set; } = new List<Product>()
        {
            new Product {Name = "prod0", Price = 0.99M },
            new Product {Name = "prod1", Price = 1.99M },
            new Product {Name = "prod2", Price = 2.99M }
        };
        public List<Order> Orders { get; set; } = new List<Order>()
        {
             new Order {UserId = 0,ProductId = 0,Quantity = 1},
             new Order {UserId = 0,ProductId =1,Quantity = 2},
             new Order {UserId = 1,ProductId =2,Quantity = 1}
        };
    }
}
