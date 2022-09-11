using Domain.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System;

namespace Aplication.Services
{
    /// <summary>
    /// OrderService
    /// </summary>
    public class OrderService
    {
        /// <summary>
        /// IOrderRepository variable
        /// </summary>
        private readonly IOrderRepository _orderRepository;
        /// <summary>
        /// IProductRepository variable
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        ///OrderService constructor
        /// </summary>
        /// <param name="or">IOrderRepository</param>
        /// <param name="pr">IProductRepository</param>
        public OrderService(IOrderRepository or, IProductRepository pr)
        {
            _orderRepository = or;
            _productRepository = pr;
        }
        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="product_name"></param>
        /// <param name="quantity"></param>
        /// <returns>String result</returns>
        public string CreateOrder(int user_id, string product_name, int quantity)
        {
            Product product = _productRepository.GetProductByName(product_name);

            if(product != null)
            {
                Order order = new Order { ProductId = product.Id, Quantity = quantity, UserId = user_id };

                _orderRepository.AddOrder(order);

                return "Order is completed";
            }
            return "Product name is incorrect";
        }

        /// <summary>
        /// Get orders history list 
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns>List of orders</returns>
        public List<string> GetOrderHistory(int user_id)
        {
            List<Order> orders =  _orderRepository.GetOrdersByUserId(user_id);

            List<string> strings = new List<string>();

            foreach (var order in orders)
            {
                Product pr = _productRepository.GetProductById(order.ProductId);
                if (pr == null) strings.Add($"Order with id:{order.Id} has inncorrect product id");
                else strings.Add($"Product id: {order.ProductId},product name: {pr.Name}, price: {pr.Price},quantity: {order.Quantity}, total sum: {pr.Price * order.Quantity} ");

            }

            return strings;
        }
        /// <summary>
        /// Cancel order
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>String result</returns>
        public string CancelOrder(int order_id)
        {
            Order order = _orderRepository.GetOrderById(order_id);

            if (order == null) return "No order with such id";

            if (order.Status == OrderStatus.Received) return "Order is already received by customer";

            order.Status = OrderStatus.CanceledByUser;
            return "Order cancelled successfuly";
        }
        /// <summary>
        /// Set order's status received
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>String result</returns>
        public string SetStatusReceived(int order_id)
        {
            Order order = _orderRepository.GetOrderById(order_id);

            if (order == null) return "No order with such id";

            if (order.Status == OrderStatus.CanceledByUser) return "Order is already canceled";

            order.Status = OrderStatus.Received;
            return "Order status changed successfuly";
        }
        /// <summary>
        /// Change order's status
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="new_order_status"></param>
        /// <returns>string result</returns>
        public string ChangeOrderStatus(int order_id,string new_order_status)
        {
            Order order = _orderRepository.GetOrderById(order_id);
            if (order == null) return "No order with such id";

            OrderStatus current_status = order.Status;

            Enum.TryParse(new_order_status, out OrderStatus order_status);

            if (order_status == current_status + 1)
            {
                order.Status = order_status;
                return "Order status was successfully changed";
            }
            else
                return "New order status is inncorrect";
        }
    }
}
