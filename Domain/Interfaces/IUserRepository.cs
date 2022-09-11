using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    /// <summary>
    /// IUserRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Bool result</returns>
        bool AddUser(User user);
        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <returns>List of users </returns>
        List<User> GetAllUsers();
        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User by id</returns>
        User GetUserById(int userId);
        /// <summary>
        /// GetUserByEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User by email</returns>
        User GetUserByEmail(string email);
        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="new_email"></param>
        /// <param name="new_balance"></param>
        /// <param name="new_password"></param>
        /// <returns>Bool result</returns>
        bool UpdateUser(int user_id, string new_email, decimal new_balance, string new_password);
    }
}
