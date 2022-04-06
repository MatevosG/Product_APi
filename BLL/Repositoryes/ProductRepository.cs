using BLL.Contracts;
using BLL.Models;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly IDistributedCache _cache;
        public ProductRepository(ProductDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache; 
        }
        public Product CreateProduct(ProductDto productDto)
        {
            var product = MapProduct(productDto);
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        public Product MapProduct(ProductDto productDto)
        {
            Product product = new Product();
            product.Id = productDto.Id; 
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.ProductType = productDto.ProductType;
            return product;
        }
        public void DeleteProduct(int id)
        {
            var productforDelete = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(productforDelete);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
             IEnumerable<Product> products;
            if (string.IsNullOrEmpty(_cache.GetString("products")))
            {
                products = _context.Products;
                string productsForCach = JsonConvert.SerializeObject(products);
                _cache.SetString("products", productsForCach);
            }
            else
            {
                string productsFromCech = _cache.GetString("products");
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsFromCech);
            }
            return products;
        }

        public Product GetById(int id)
        {
            if (!string.IsNullOrEmpty(_cache.GetString("products")))
            {
                string productsFromCech = _cache.GetString("products");
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsFromCech);
                var  product = products.FirstOrDefault(x => x.Id == id);

                if (product != null) 
                    return product;
                
                product = _context.Products.FirstOrDefault(x => x.Id == id);
                string productForCach = JsonConvert.SerializeObject(product);
                _cache.SetString("products", productForCach);
                return product;
            }
           return null;
        }

        public Product UpdateProduct(ProductDto productDto)
        {
            var entityprods = _context.Products.Where(x => x.Id == productDto.Id);
            foreach (var item in entityprods)
            {
                item.ProductType = productDto.ProductType;
                item.Price = productDto.Price;
                item.Name = productDto.Name;
                item.Description = productDto.Description;
            }
            _context.SaveChanges();
            return GetById(productDto.Id);
        }
    }
}
