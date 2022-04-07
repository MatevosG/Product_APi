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
       // private readonly IDistributedCache _cache;
        private ICacheRepository _cacheRepository;
        private const string MyCacheKey = "MyKey";
        public ProductRepository(ProductDbContext context, IDistributedCache cache,ICacheRepository cacheRepository )
        {
            _context = context;
            //_cache = cache; 
            _cacheRepository = cacheRepository;
        }
        public Product CreateProduct(ProductDto productDto)
        {
            _cacheRepository.AddCache(MyCacheKey, productDto);
            _cacheRepository.Refresh(MyCacheKey);
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
            //if (string.IsNullOrEmpty(_cache.GetString("products")))
            //{
            //    products = _context.Products;
            //    string productsForCach = JsonConvert.SerializeObject(products);
            //    _cache.SetString("products", productsForCach);
            //}
            //else
            //{
            //    string productsFromCech = _cache.GetString("products");
            //    products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsFromCech);
            //}
            //return products;
            var productsFrommCache = _cacheRepository.GetCacheResponse(MyCacheKey);
            if (!string.IsNullOrEmpty(productsFrommCache))
            {
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsFrommCache);
                return products;
            }

             products = _context.Products;
            _cacheRepository.AddCache(MyCacheKey, products);
            return products;
        }

        public Product GetById(int id)
        {
            //if (!string.IsNullOrEmpty(_cache.GetString(MyCacheKey)))
            //{
            //    string productsFromCech = _cache.GetString(MyCacheKey);
            //    var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsFromCech);
            //    var  product = products.FirstOrDefault(x => x.Id == id);

            //    if (product != null) 
            //        return product;
                
            //    product = _context.Products.FirstOrDefault(x => x.Id == id);
            //    string productForCach = JsonConvert.SerializeObject(product);
            //    _cache.SetString("products", productForCach);
            //    return product;
            //}
            var productsFromCache = _cacheRepository.GetCacheResponse(MyCacheKey);
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsFromCache);
            var product = products.FirstOrDefault(x=>x.Id==id);
            if (product != null)
                return product;
            product = _context.Products.FirstOrDefault(x => x.Id == id);
            _cacheRepository.AddCache(MyCacheKey,product);
            return product;
        }

        public Product UpdateProduct(ProductDto productDto)
        {
            //var producstFromCache = _cacheRepository.GetCacheResponse(MyCacheKey);
            //var productsFromCache = JsonConvert.DeserializeObject<List<Product>>(producstFromCache);
            //var productForUpdate = productsFromCache.FirstOrDefault(x => x.Id==productDto.Id);

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
