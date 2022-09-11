using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    /// <summary>
    /// IProductRepository
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <returns>List of products</returns>
        List<Product> GetAllProducts();
        /// <summary>
        /// GetProductByName
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Product</returns>
        Product GetProductByName(string name);

        /// <summary>
        /// GetProductById
        /// </summary>
        /// <param name="prodId"></param>
        /// <returns>Product</returns>
        Product GetProductById(int prodId);

        /// <summary>
        /// AddProduct
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Bool result</returns>
        bool AddProduct(Product product);
        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <param name="prodId"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns>Bool result</returns>
        bool UpdateProduct(int prodId, string name, decimal price);
    }
}
