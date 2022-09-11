using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace ConsoleUI.Menus
{
    /// <summary>
    /// Admin menu class : UserMenu
    /// </summary>
    public class AdminMenu : UserMenu
    {
        /// <summary>
        /// Admin menu constructor
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userRespository"></param>
        /// <param name="productRepository"></param>
        /// <param name="orderRepository"></param>
        public AdminMenu(User user, IUserRepository userRespository, IProductRepository productRepository, IOrderRepository orderRepository) : base(user, userRespository, productRepository, orderRepository) { }

        /// <summary>
        /// Get list of users
        /// </summary>
        protected void SeeUsersInfo()
        {
            List<User> users = _userService.GetAllUsers();

            if (users.Count == 0)
                Console.WriteLine("No user");
            else
                foreach (User user in users)
                    Console.WriteLine(user.ToString());
        }
        /// <summary>
        /// Change data of user
        /// </summary>
        protected void ChangeUserData()
        {
            try
            {
                Console.WriteLine("Enter user id");
                int user_id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter a new email");
                string new_email = Console.ReadLine();

                Console.WriteLine("Enter a new password");
                decimal new_password = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter a new balance");
                string new_balance = Console.ReadLine();

                Console.WriteLine(_userService.ChangeUserData(user_id, new_email, new_password, new_balance));
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
        /// Get list of orders of user
        /// </summary>
        protected void SeeOrdersOfUser()
        {
            try
            {
                Console.WriteLine("Please enter user id:");
                int user_id = Convert.ToInt32(Console.ReadLine());

                List<string> ordersHistory = _orderService.GetOrderHistory(user_id);

                if (ordersHistory.Count == 0)
                    Console.WriteLine("Your order history is empty");
                else
                    foreach (var order in ordersHistory)
                        Console.WriteLine(order);
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
        /// Add new product
        /// </summary>
        protected void AddProduct()
        {
            try
            {

                Console.WriteLine("Name of new product:");
                string name = Console.ReadLine();

                Console.WriteLine("Price of the product");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine(_productService.AddProduct(name, price));
            }
            catch (OverflowException)
            {
                Console.WriteLine("Outside the range of the Int32 type.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Not valid price!");
            }
        }
        /// <summary>
        /// Modify data od product
        /// </summary>
        protected void ModifyProduct()
        {
            try
            {
                Console.WriteLine("Id of product");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("New name of product:");
                string name = Console.ReadLine();
                Console.WriteLine("New price of product:");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine(_productService.ModifyProduct(id, name, price));
            }
            catch (OverflowException)
            {
                Console.WriteLine("Outside the range of the Int32 type.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Not valid data!");
            }

        }
        /// <summary>
        /// Change staus od order
        /// </summary>
        protected void ChangeOrderStatus()
        {
            try
            {
                Console.WriteLine("Enter order id:");
                int order_id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter new order status:");
                string order_status = Console.ReadLine();

                Console.WriteLine(_orderService.ChangeOrderStatus(order_id, order_status));
            }

            catch (OverflowException)
            {
                Console.WriteLine("Outside the range of the Int32 type.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Not valid data!");
            }
        }



        /// <summary>
        /// Prints options of admin user
        /// </summary>
        protected override void PrintOptions()
        {
            Console.Clear();

            Console.WriteLine("1) See users info");
            Console.WriteLine("2) Change user's data");
            Console.WriteLine("3) See products");

            Console.WriteLine("4) Find product by name");
            Console.WriteLine("5) Create order");

            Console.WriteLine("6) Add product");
            Console.WriteLine("7) Modify product");
            Console.WriteLine("8) See orders of user");
            Console.WriteLine("9) Change order status");
            Console.WriteLine("10) Log Out");
            Console.WriteLine("11) Exit");
        }
        /// <summary>
        /// List of admin operators
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, Action> GetOperators()
        {
            var GuestOperators = new Dictionary<string, Action>
            {
                { "1", SeeUsersInfo },
                { "2", ChangeUserData },
                { "3", GetAllProducts },

                { "4", SearchProductByName },
                { "5", CreateOrder },

                { "6", AddProduct },
                { "7", ModifyProduct },
                { "8", SeeOrdersOfUser },
                { "9", ChangeOrderStatus },
                { "10", LogOut },
                { "11", Exit }
            };

            return GuestOperators;

        }
    }
}
