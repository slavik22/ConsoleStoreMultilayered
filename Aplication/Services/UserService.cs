using Domain.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aplication.Services
{
    public class UserService
    {
        /// <summary>
        /// IUserRepository variable
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        ///UserService constructor
        /// </summary>
        /// <param name="ur">IUserRepository</param>
        public UserService(IUserRepository ur) => _userRepository = ur;
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>User</returns>
        public User Register(string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is null)
            {
                User user = new User() { Email = email, Password = password, Balance = 0 };

                return _userRepository.AddUser(user) ? user : null;
            }
            return null;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>User</returns>
        public User Login(string email, string password)
        {
            return _userRepository.GetAllUsers().FirstOrDefault(u => (u.Email == email) && (u.Password == password));

        }
        /// <summary>
        /// Get money balance of user
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns>User's money balance</returns>
        public decimal GetUserBalance(int user_id)
        {
            return _userRepository.GetUserById(user_id).Balance;
        }
        /// <summary>
        /// Change data of user
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="new_email"></param>
        /// <param name="new_balance"></param>
        /// <param name="new_password"></param>
        /// <returns>Strinf result</returns>
        public string ChangeUserData(int user_id, string new_email, decimal new_balance, string new_password)
        {
           return _userRepository.UpdateUser(user_id,new_email,new_balance,new_password) ? "Password was updated successfuly" : "No user with such id";
        }
        /// <summary>
        /// Get list of all users
        /// </summary>
        /// <returns>List of users</returns>
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}
