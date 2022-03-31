using BLL.Contracts;
using BLL.Models;
using DAL;
using DAL.Entities;
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
        public ProductRepository(ProductDbContext context)
        {
            _context = context;
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
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.ProductType = productDto.ProductType;
            return product;
        }
        public void DeleteProduct(int id)
        {
            var productforDelete = _context.Products.FirstOrDefault(x=>x.Id==id);
            _context.Products.Remove(productforDelete);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id==id);
        }

        public Product UpdateProduct(ProductDto productDto)
        {
            var product = MapProduct(productDto);
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }
    }
}
