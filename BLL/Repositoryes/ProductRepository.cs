using BLL.Cache;
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
            return _context.Products;
        }

        public Product GetById(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            return product;
        }

        public Product UpdateProduct(ProductDto productDto)
        {
            var entityprod = _context.Products.FirstOrDefault(x => x.Id == productDto.Id);

            entityprod.ProductType = productDto.ProductType;
            entityprod.Price = productDto.Price;
            entityprod.Name = productDto.Name;
            entityprod.Description = productDto.Description;

            //_context.Entry(entityprod).State = EntityState.Modified;

            _context.SaveChanges();
            return entityprod;
        }
    }
}
