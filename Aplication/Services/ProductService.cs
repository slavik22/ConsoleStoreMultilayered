using Domain.Interfaces;
using Domain.Entities;
using System.Collections.Generic;

namespace Aplication.Services
{
    /// <summary>
    /// ProductService class
    /// </summary>
    public class ProductService
    {
        /// <summary>
        /// IProductRepository variable
        /// </summary>
        private readonly IProductRepository _productRepository;
        /// <summary>
        /// ProductService constructor
        /// </summary>
        /// <param name="pr">IProductRepository</param>
        public ProductService(IProductRepository pr)
        {
            _productRepository = pr;
        }
        /// <summary>
        /// Get list of all products
        /// </summary>
        /// <returns>List of products</returns>
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
        /// <summary>
        /// Get product by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>String results</returns>
        public string SearchProductByName(string name)
        {
            Product product = _productRepository.GetProductByName(name);

            if (product != null)
                return product.ToString();
            else
                return "No product with such name";
        }
        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns>String result</returns>
        public string AddProduct(string name, decimal price)
        {
            if (_productRepository.GetProductByName(name) != null) return "There is already product with such name";

            Product pr = new Product() { Name = name, Price = price };
            _productRepository.AddProduct(pr);

            return "Product was successfuly added";
        }
        /// <summary>
        /// Modify product
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns>String result</returns>
        public string ModifyProduct(int product_id, string name, decimal price)
        {
            if ( _productRepository.GetProductById(product_id) == null) return "No product with such id";

            _productRepository.UpdateProduct(product_id, name, price);

            return "Product was successfuly updated";
        }
    }
}
