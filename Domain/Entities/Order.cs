using System.Threading;

namespace Domain.Entities
{
    /// <summary>
    /// Order entity
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Temporary id counter
        /// </summary>
        private static int count = 0;

        /// <summary>
        /// Constructor for initiating id
        /// </summary>
        public Order()
        {
            Id = count;
            Interlocked.Increment(ref count);

        }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of user
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Id of product
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Quantity of product
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Status of order
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.New;
    }

    /// <summary>
    /// Enum of possible order statuses
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// New order status
        /// </summary>
        New,
        /// <summary>
        /// CanceledByAdmin status
        /// </summary>
        CanceledByAdmin = 1,
        /// <summary>
        /// PaymentReceived status
        /// </summary>
        PaymentReceived = 1,
        /// <summary>
        /// Sent status
        /// </summary>
        Sent,
        /// <summary>
        /// Complete status
        /// </summary>
        Complete,
        /// <summary>
        /// Received status
        /// </summary>
        Received,
        /// <summary>
        /// CanceledByUser status
        /// </summary>
        CanceledByUser
    }
}
