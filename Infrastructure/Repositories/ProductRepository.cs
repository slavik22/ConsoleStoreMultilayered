using Domain.Entities;
using Domain.Interfaces;

using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// ProductRepository
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// AddProduct
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Bool result</returns>
        public bool AddProduct(Product product)
        {
            AplicationDbContext.Products.Add(product);
            return true;
        }
        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <returns>List of products</returns>
        public List<Product> GetAllProducts()
        {
            return AplicationDbContext.Products;
        }
        /// <summary>
        /// GetProductById
        /// </summary>
        /// <param name="prodId"></param>
        /// <returns>Product</returns>
        public Product GetProductById(int prodId)
        {
            return AplicationDbContext.Products.FirstOrDefault(prod => prod.Id == prodId);
        }
        /// <summary>
        /// GetProductByName
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Product</returns>
        public Product GetProductByName(string name)
        {
            return AplicationDbContext.Products.FirstOrDefault(prod => prod.Name == name);
        }
        /// <summary>
        /// UpdateProduct
        /// </summary>
        /// <param name="prodId"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns>Bool result</returns>
        public bool UpdateProduct(int prodId, string name, decimal price)
        {
            Product pr = GetProductById(prodId);

            pr.Name = name;
            pr.Price = price;

            return true;
        }
    }
}
