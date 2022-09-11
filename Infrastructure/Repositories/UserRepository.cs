using Domain.Interfaces;
using Domain.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// UserRepository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <returns>List of users </returns>
        public List<User> GetAllUsers()
        {
            return AplicationDbContext.Users;
        }

        /// <summary>
        /// AddUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Bool result</returns>
        public bool AddUser(User user)
        {
            AplicationDbContext.Users.Add(user);
            return true;
        }

        /// <summary>
        /// GetUserByEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User by email</returns>
        public User GetUserByEmail(string email)
        {
            return AplicationDbContext.Users.FirstOrDefault(user => user.Email == email);
        }
        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User by id</returns>
        public User GetUserById(int userId)
        {
            return AplicationDbContext.Users.FirstOrDefault(user => user.Id == userId);
        }
        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="new_email"></param>
        /// <param name="new_balance"></param>
        /// <param name="new_password"></param>
        /// <returns>Bool result</returns>
        public bool UpdateUser(int user_id, string new_email, decimal new_balance, string new_password)
        {
            User targetUser = GetUserById(user_id);

            if (targetUser == null) return false;

            targetUser.Email = new_email;
            targetUser.Password = new_password;
            targetUser.Balance = new_balance;

            return true;
            
        }
    }
}
