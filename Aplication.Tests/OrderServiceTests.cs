using Aplication.Services;
using Domain.Interfaces;
using Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Aplication.Tests
{
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;

        private readonly Mock<IProductRepository> _mockedProductRepository = new Mock<IProductRepository>();
        private readonly Mock<IOrderRepository> _mockedOrderRepository = new Mock<IOrderRepository>();

        public OrderServiceTests()
        {
            _orderService = new OrderService(_mockedOrderRepository.Object, _mockedProductRepository.Object);
        }

        [Theory]
        [InlineData(0, "prod0", "Order is completed")]
        [InlineData(1, "prod100", "Product name is incorrect")]
        public void CreateOrder_ReturnsCorrectStringResult(int user_id, string product_name, string expected_result)
        {
            //Arrange
            _mockedProductRepository.Setup(a => a.GetProductByName(product_name))
                .Returns(Products.FirstOrDefault(p => p.Name == product_name));
            _mockedOrderRepository.Setup(a => a.AddOrder(new Order { }))
                .Returns(true);
            // Act

            string actual_result = _orderService.CreateOrder(user_id, product_name, 1);

            //Assert
            Assert.Equal(expected_result, actual_result);
        }


        [Fact]
        public void GetOrderHistory_ReturnsCorrectOrdersList()
        {
            int user_id = 0;

            //Arrange
            _mockedOrderRepository.Setup(a => a.GetOrdersByUserId(user_id))
                .Returns(Orders.Where(ord => ord.UserId == user_id).ToList());
            // Act

            var actual_result = _orderService.GetOrderHistory(user_id);

            //Assert
            Assert.NotEmpty(actual_result);
        }

        [Theory]
        [InlineData(0, "Order is already received by customer")]
        [InlineData(1, "Order cancelled successfuly")]
        [InlineData(int.MaxValue, "No order with such id")]

        public void CancelOrder_ReturnsCorrectStringResult(int order_id, string expected_result)
        {
            //Arrange
            _mockedOrderRepository.Setup(a => a.GetOrderById(order_id))
                .Returns(Orders.FirstOrDefault(or => or.Id == order_id));
            // Act

            var actual_result = _orderService.CancelOrder(order_id);

            //Assert
            Assert.Equal(expected_result,actual_result);
        }

        [Theory]
        [InlineData(0, "Order status changed successfuly")]
        [InlineData(1, "Order is already canceled")]
        [InlineData(int.MaxValue, "No order with such id")]

        public void SetStatusReceived_ReturnsCorrectStringResult(int order_id, string expected_result)
        {
            //Arrange
            _mockedOrderRepository.Setup(a => a.GetOrderById(order_id))
                .Returns(Orders.FirstOrDefault(or => or.Id == order_id));
            // Act

            var actual_result = _orderService.SetStatusReceived(order_id);

            //Assert
            Assert.Equal(expected_result, actual_result);
        }


        [Theory]
        [InlineData(2, "PaymentReceived", "Order status was successfully changed")]
        [InlineData(3, "Complete", "New order status is inncorrect")]
        [InlineData(int.MaxValue, "CanceledByAdmin", "No order with such id")]

        public void ChangeOrderStatus_ReturnsCorrectStringResult(int order_id, string new_order_status, string expected_result)
        {
            //Arrange
            _mockedOrderRepository.Setup(a => a.GetOrderById(order_id))
                .Returns(Orders.FirstOrDefault(or => or.Id == order_id));
            // Act

            var actual_result = _orderService.ChangeOrderStatus(order_id, new_order_status);

            //Assert
            Assert.Equal(expected_result, actual_result);
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
            new Product {Id = 0,Name = "prod0", Price = 0.99M },
            new Product {Id = 1,Name = "prod1", Price = 1.99M },
            new Product {Id = 2,Name = "prod2", Price = 2.99M }
        };
        public List<Order> Orders { get; set; } = new List<Order>()
        {
             new Order {Id = 0, UserId = 0,ProductId = 0,Quantity = 1, Status = OrderStatus.Received},
             new Order {Id = 1,UserId = 0,ProductId =1,Quantity = 2, Status = OrderStatus.CanceledByUser},
             new Order {Id = 2,UserId = 1,ProductId =2,Quantity = 1},
             new Order {Id = 3,UserId = 1,ProductId =2,Quantity = 1, Status = OrderStatus.PaymentReceived}
        };
    }
}
