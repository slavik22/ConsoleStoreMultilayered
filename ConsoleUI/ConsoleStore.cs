using Domain.Interfaces;
using Infrastructure.Repositories;
using Domain.Entities;
using ConsoleUI.Menus;
using System;


namespace ConsoleUI
{
    /// <summary>
    /// ConsoleStore class
    /// </summary>
    public class ConsoleStore
    {
        /// <summary>
        /// IUserRepository variable
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// IOrderRepository variable
        /// </summary>
        private readonly IOrderRepository _orderRepository;
        /// <summary>
        /// IProductRepository variable
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// ConsoleStore constructor
        /// </summary>
        public ConsoleStore()
        {
            _userRepository = new UserRepository();
            _orderRepository = new OrderRepository();
            _productRepository = new ProductRepository();

            menu = new GuestMenu(_userRepository, _productRepository);

        }
        /// <summary>
        /// Menu variable
        /// </summary>
        private Menu menu;

        /// <summary>
        /// Start of console menu
        /// </summary>
        public void Start()
        {

            (menu as GuestMenu).NotifyOfLogginIn += LogIn;

            while (menu.IsActive)
            {
                menu.ChooseOption();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            }
        }
        /// <summary>
        /// LogIn event
        /// </summary>
        /// <param name="user"></param>
        public void LogIn(User user)
        {
            if (user.IsAdmin)
            {
                menu = new AdminMenu(user, _userRepository, _productRepository, _orderRepository);
                (menu as AdminMenu).NotifyOfLoggingOut += LogOut;
            }
            else
            {
                menu = new UserMenu(user, _userRepository, _productRepository, _orderRepository);
                (menu as UserMenu).NotifyOfLoggingOut += LogOut;
            }
        }
        /// <summary>
        /// LogOut event
        /// </summary>
        public void LogOut()
        {
            menu = new GuestMenu(_userRepository, _productRepository);
            (menu as GuestMenu).NotifyOfLogginIn += LogIn;
        }

    }
}
