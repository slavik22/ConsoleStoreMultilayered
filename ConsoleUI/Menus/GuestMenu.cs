using System;
using System.Collections.Generic;

using Aplication.Services;
using Domain.Interfaces;
using Domain.Entities;

namespace ConsoleUI.Menus
{
    /// <summary>
    /// GuestMenu inheritates abstract Menu
    /// </summary>
    public class GuestMenu : Menu
    {
        /// <summary>
        /// Override isActive
        /// </summary>
        public override bool IsActive { get; set; } = true;
        /// <summary>
        /// ProductService variable
        /// </summary>
        private readonly ProductService _productService;
        /// <summary>
        /// UserService variable
        /// </summary>
        private readonly UserService _userService;
        /// <summary>
        /// LogInHendler
        /// </summary>
        /// <param name="user"></param>
        public delegate void LogInHendler(User user);
        /// <summary>
        /// NotifyOfLogginIn
        /// </summary>
        public event LogInHendler NotifyOfLogginIn;
        /// <summary>
        /// GuestMenu constructor
        /// </summary>
        /// <param name="userRespository">IUserRepository</param>
        /// <param name="productRepository">IProductRepository</param>
        public GuestMenu(IUserRepository userRespository, IProductRepository productRepository)
        {
            _productService = new ProductService(productRepository);
            _userService = new UserService(userRespository);
        }

        private void GetAllProducts()
        {
            List<Product> products = _productService.GetAllProducts();

            foreach (var product in products)
                Console.WriteLine(product.ToString());
        }
        /// <summary>
        /// SearchProductByName
        /// </summary>
        private void SearchProductByName()
        {
            Console.WriteLine("Name of product:");
            string name = Console.ReadLine();

            if (!String.IsNullOrEmpty(name))
                Console.WriteLine(_productService.SearchProductByName(name));
            else
                Console.WriteLine("Name is empty");

        }
        /// <summary>
        /// Register new user
        /// </summary>
        private void Register()
        {
            Console.WriteLine("Email:");
            string email = Console.ReadLine();

            if (!email.Contains("@"))
            {
                Console.Write("Email should contain '@' symbol.");
                return;
            }

            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            User newUser = _userService.Register(email, password);

            if (newUser == null)
            {
                Console.WriteLine("Email is already registered");
                return;
            }

            NotifyOfLogginIn?.Invoke(newUser);
            Console.WriteLine("Registered");


        }
        /// <summary>
        /// Login user
        /// </summary>
        private void Login()
        {
            Console.WriteLine("Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            User user = _userService.Login(email, password);

            if (user != null)
            {
                Console.WriteLine($"WellCome back! {user.Email}");
                NotifyOfLogginIn?.Invoke(user);
            }
            else
            {
                Console.WriteLine("Wrong email or password");
            }
        }

        /// <summary>
        /// Override PrintOptions
        /// </summary>
        protected override void PrintOptions()
        {
            Console.Clear();

            Console.WriteLine("1) See products");
            Console.WriteLine("2) Find product by name");
            Console.WriteLine("3) Log In");
            Console.WriteLine("4) Register");
            Console.WriteLine("5) Exit");
        }

        /// <summary>
        /// Dictionary of guest's options
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, Action> GetOperators()
        {
            var GuestOperators = new Dictionary<string, Action>
            {
                { "1", GetAllProducts },
                { "2", SearchProductByName },
                { "3", Login },
                { "4", Register },
                { "5", Exit }
            };

            return GuestOperators;

        }
    }
}
