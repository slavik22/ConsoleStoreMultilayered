using Domain.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// OrderRepository
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// AddOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Bool result</returns>
        public bool AddOrder(Order order)
        {
            AplicationDbContext.Orders.Add(order);
            return true;
        }
        /// <summary>
        /// GetOrderById
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Order</returns>
        public Order GetOrderById(int orderId)
        {
            return AplicationDbContext.Orders.FirstOrDefault(ord => ord.Id == orderId);
        }
        /// <summary>
        /// GetOrdersByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of user's orders</returns>
        public List<Order> GetOrdersByUserId(int userId)
        {
            return AplicationDbContext.Orders.Where(ord => ord.UserId == userId).ToList();
        }
        /// <summary>
        /// SetNewOrderStatus
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns>Bool result</returns>
        public bool SetNewOrderStatus(int orderId, OrderStatus status)
        {
            Order order = GetOrderById(orderId);

            if (order != null)
            {
                order.Status = status;
                return true;
            }

            return false;
        }
    }
}
