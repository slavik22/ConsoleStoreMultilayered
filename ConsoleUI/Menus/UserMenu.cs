using Aplication.Services;
using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace ConsoleUI.Menus
{
    /// <summary>
    /// GuestMenu inheritates abstract Menu
    /// </summary>
    public class UserMenu : Menu
    {
        /// <summary>
        /// Override isActive
        /// </summary>
        public override bool IsActive { get; set; } = true;
        /// <summary>
        /// User variable
        /// </summary>
        protected readonly User user;

        /// <summary>
        /// ProductService variable
        /// </summary>
        protected readonly ProductService _productService;
        /// <summary>
        /// UserService variable
        /// </summary>
        protected readonly UserService _userService;
        /// <summary>
        /// OrderService variable
        /// </summary>
        protected readonly OrderService _orderService;
        /// <summary>
        /// LogOutHendler
        /// </summary>
        public delegate void LogOutHendler();
        /// <summary>
        /// NotifyOfLoggingOut
        /// </summary>
        public event LogOutHendler NotifyOfLoggingOut;

        /// <summary>
        /// UserMenu constructor
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="userRespository"></param>
        /// <param name="productRepository"></param>
        /// <param name="orderRepository"></param>
        public UserMenu(User newUser, IUserRepository userRespository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            user = newUser;
            _productService = new ProductService(productRepository);
            _userService = new UserService(userRespository);
            _orderService = new OrderService(orderRepository, productRepository);

        }
        /// <summary>
        /// Get list of products
        /// </summary>
        protected void GetAllProducts()
        {
            List<Product> products = _productService.GetAllProducts();

            foreach (var product in products)
                Console.WriteLine(product.ToString());
        }
        /// <summary>
        /// Get product by name
        /// </summary>
        protected void SearchProductByName()
        {
            Console.WriteLine("Name of product:");
            string name = Console.ReadLine();

            if (!String.IsNullOrEmpty(name))
                Console.WriteLine(_productService.SearchProductByName(name));
            else
                Console.WriteLine("Name is empty");

        }
        /// <summary>
        /// Create order
        /// </summary>
        protected void CreateOrder()
        {
            Console.WriteLine("Input name of the product:");
            string product_name = Console.ReadLine();

            Console.WriteLine("Quantity of porducts");
            try
            {
                int quantity = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(_orderService.CreateOrder(user.Id, product_name, quantity));

            }
            catch (OverflowException)
            {
                Console.WriteLine("Outside the range of the Int32 type.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Not valid Id!");
            }


        }

        /// <summary>
        /// See user's order history
        /// </summary>
        protected void SeeOrderHistory()
        {
            List<string> ordersHistory = _orderService.GetOrderHistory(user.Id);

            if (ordersHistory.Count == 0)
                Console.WriteLine("Your order history is empty");
            else
                foreach (var order in ordersHistory)
                    Console.WriteLine(order);
        }

        /// <summary>
        /// LogOut
        /// </summary>
        protected void LogOut() => NotifyOfLoggingOut?.Invoke();

        /// <summary>
        /// Cancel order
        /// </summary>
        protected void CancelOrder()
        {
            try
            {
                Console.WriteLine("Enter order id:");
                int order_id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(_orderService.CancelOrder(order_id));
            }
            catch (OverflowException)
            {
                Console.WriteLine("Outside the range of the Int32 type.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Not valid Id!");
            }
        }
        /// <summary>
        /// Set order's status received
        /// </summary>
        protected void SetStatusReceived()
        {
            try
            {
                Console.WriteLine("Enter order id:");
                int order_id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(_orderService.SetStatusReceived(order_id));
            }
            catch (OverflowException)
            {
                Console.WriteLine("Outside the range of the Int32 type.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Not valid Id!");
            }
        }

        /// <summary>
        /// Check user's balance
        /// </summary>
        protected void CheckBalance()
        {
            Console.WriteLine($"Your balance: {_userService.GetUserBalance(user.Id)}");
        }
        /// <summary>
        /// Change user's password
        /// </summary>
        protected void ChangePassword()
        {
            Console.WriteLine("Enter a new password");
            string new_password = Console.ReadLine();

            Console.WriteLine(_userService.ChangeUserData(user.Id, user.Email, user.Balance, new_password));
        }

        /// <summary>
        /// Print user's options
        /// </summary>
        protected override void PrintOptions()
        {
            Console.Clear();

            Console.WriteLine("1) See products");
            Console.WriteLine("2) Find product by name");
            Console.WriteLine("3) Create order");
            Console.WriteLine("4) See order history");
            Console.WriteLine("5) Set 'Received' status");
            Console.WriteLine("6) Cancel order");
            Console.WriteLine("7) Change password");
            Console.WriteLine("8) Check balance");
            Console.WriteLine("9) Log Out");
            Console.WriteLine("10) Exit");
        }

        /// <summary>
        /// User's list of operators
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, Action> GetOperators()
        {
            var GuestOperators = new Dictionary<string, Action>
            {
                { "1", GetAllProducts },
                { "2", SearchProductByName },
                { "3", CreateOrder },
                { "4", SeeOrderHistory },
                { "5", SetStatusReceived },
                { "6", CancelOrder },
                { "7", ChangePassword },
                { "8", CheckBalance },
                { "9", LogOut },
                { "10", Exit }
            };

            return GuestOperators;

        }
    }
}
