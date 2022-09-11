using System.Threading;

namespace Domain.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// Temporary id counter
        /// </summary>
        private static int count = 0;
        /// <summary>
        /// Constructor for initiating id
        /// </summary>
        public User()
        {
            Id = count;
            Interlocked.Increment(ref count);
        }
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Email of user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password of user
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Bool if admin
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// money balance of user
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Overrided toString
        /// </summary>
        public override string ToString()
        {
            string Role = IsAdmin ? "Admin" : "User";
            return $"Id: {Id}, Email: {Email}, password: {Password}, Role: {Role}";
        }

    }
}
