using System.Threading;

namespace Domain.Entities
{
    /// <summary>
    /// Product entity
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Temporary id counter
        /// </summary>
        private static int count = 0;

        /// <summary>
        /// Constructor for initiating id
        /// </summary>
        public Product()
        {
            Id = count;
            Interlocked.Increment(ref count);
        }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Price of product
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Overrided toString
        /// </summary>
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Price: {Price}";
        }

    }
}
