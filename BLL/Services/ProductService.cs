using BLL.Cache;
using BLL.Contracts;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IProductRepository _productRepository;

        public ProductService(ICacheRepository cacheRepository, IProductRepository productRepository)
        {
            _cacheRepository = cacheRepository;
            _productRepository = productRepository;
        }
        
        public Product CreateProduct(ProductDto productDto)
        {
            var product = _productRepository.CreateProduct(productDto);
            var productFromCache = _cacheRepository.SetOrUpdate<Product>(product.Id.ToString(),product);
            return productFromCache; 
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
            _cacheRepository.Delete<Product>(id.ToString());
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll(); 
        }

        public Product GetById(int id)
        {
            var productFromCache = _cacheRepository.Get<Product>(id.ToString());
            if (productFromCache!=null)
            {
                return productFromCache;
            }
            return _productRepository.GetById(id);
        }

        public Product UpdateProduct(ProductDto productDto)
        {
            var productForUpdate = _productRepository.UpdateProduct(productDto);
            productForUpdate= _cacheRepository.SetOrUpdate(productDto.Id.ToString(), productForUpdate);
            return productForUpdate;
        }
    }
}
