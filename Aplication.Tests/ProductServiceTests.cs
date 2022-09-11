using Aplication.Services;
using Domain.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

namespace Aplication.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;

        private readonly Mock<IProductRepository> _mockedProductRepository = new Mock<IProductRepository>();

        public ProductServiceTests()
        {
            _productService = new ProductService(_mockedProductRepository.Object);
        }

        [Fact]

        public void GetAllProducts_ReturnsCorrectList()
        {
            //Arrange
            _mockedProductRepository.Setup(a => a.GetAllProducts())
                .Returns(Products);

            // Act

            var productList = _productService.GetAllProducts();

            //Assert

            Assert.NotEmpty(productList);
        }



        [Theory]
        [InlineData("prod0", true )]
        [InlineData("prod100", false )]
        public void SearchProductByName_ReturnsCorrectStringResult(string name, bool productIsInList)
        {
            //Arrange
            _mockedProductRepository.Setup(a => a.GetProductByName(name))
                .Returns(Products.FirstOrDefault(p => p.Name == name));

            // Act

            string actual_result = _productService.SearchProductByName(name);

            //Assert
            if(productIsInList) Assert.NotEmpty(actual_result);
            else Assert.Equal ("No product with such name", actual_result);
        }

        [Theory]
        [InlineData("prod0", 0, "There is already product with such name")]
        [InlineData("prod100", 0, "Product was successfuly added")]
        public void AddProduct_ReturnsCorrectStringResult(string name, decimal price, string expected_result)
        {
            //Arrange
            _mockedProductRepository.Setup(a => a.GetProductByName(name))
                .Returns(Products.FirstOrDefault(p => p.Name == name));

            Product pr = new Product() { Name = name, Price = price };

            _mockedProductRepository.Setup(a => a.AddProduct(pr))
                .Returns(true);
            // Act

            string actual_result = _productService.AddProduct(name, price);

            //Assert
            Assert.Equal(expected_result, actual_result);
        }

        [Theory]
        [InlineData(0,"prod0", 0, "Product was successfuly updated")]
        [InlineData(100,"prod100", 0, "No product with such id")]
        public void UpdateProduct_ReturnsCorrectStringResult(int product_id,string name, decimal price, string expected_result)
        {
            //Arrange
            _mockedProductRepository.Setup(a => a.GetProductById(product_id))
                .Returns(Products.FirstOrDefault(p => p.Id == product_id));

            _mockedProductRepository.Setup(a => a.UpdateProduct(product_id,name,price))
                .Returns(true);
            // Act

            string actual_result = _productService.ModifyProduct(product_id, name, price);

            //Assert
            Assert.Equal(expected_result, actual_result);
        }


        [Theory]
        [InlineData(0, "prod0", 0)]
        [InlineData(1, "prod100",0)]
        public void UpdateProduct_UpdatesData(int product_id, string name, decimal price)
        {
            //Arrange
            _mockedProductRepository.Setup(a => a.GetProductById(product_id))
                .Returns(Products.FirstOrDefault(p => p.Id == product_id));

            _mockedProductRepository.Setup(a => a.UpdateProduct(product_id, name, price))
                .Returns(() =>
                {
                    Products[product_id].Name = name;
                    Products[product_id].Price = price;
                    return true;
                });
            // Act

            string actual_result = _productService.ModifyProduct(product_id, name, price);

            //Assert
            Assert.Equal("Product was successfuly updated", actual_result);

            Assert.True(Products[product_id].Name == name && Products[product_id].Price == price);
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
            new Product {Id = 0, Name = "prod0", Price = 0.99M },
            new Product {Id = 1,Name = "prod1", Price = 1.99M },
            new Product {Id = 2,Name = "prod2", Price = 2.99M }
        };
        public List<Order> Orders { get; set; } = new List<Order>()
        {
             new Order {UserId = 0,ProductId = 0,Quantity = 1},
             new Order {UserId = 0,ProductId =1,Quantity = 2},
             new Order {UserId = 1,ProductId =2,Quantity = 1}
        };
    }
}
