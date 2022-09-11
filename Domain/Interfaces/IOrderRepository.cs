using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    /// <summary>
    /// IOrderRepository
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// AddOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Bool result</returns>
        bool AddOrder(Order order);
        /// <summary>
        /// GetOrdersByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of user's orders</returns>
        List<Order> GetOrdersByUserId(int userId);
        /// <summary>
        /// GetOrderById
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Order</returns>
        Order GetOrderById(int orderId);

        /// <summary>
        /// SetNewOrderStatus
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns>Bool result</returns>
        bool SetNewOrderStatus(int orderId, OrderStatus status);
    }
}
