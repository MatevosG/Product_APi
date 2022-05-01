using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductService(ICacheRepository cacheRepository, IProductRepository productRepository, IMapper mapper)
        {
            _cacheRepository = cacheRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductDto CreateProduct(ProductDto productDto)
        {
            var productMap = _mapper.Map<Product>(productDto);   
            var product = _productRepository.CreateProduct(productMap);
            var productFromCache = _cacheRepository.SetOrUpdate<Product>(product.Id.ToString(),product);

            return _mapper.Map<ProductDto>(productFromCache); 
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
            _cacheRepository.Delete<Product>(id.ToString());
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var products = _productRepository.GetAll();
            var productMap =_mapper.Map<List<ProductDto>>(products);
            return productMap; 
        }

        public ProductDto GetById(int id)
        {
            var productFromCache = _cacheRepository.Get<ProductDto>(id.ToString());
            if (productFromCache!=null)
            {
                return productFromCache;
            }
            
            var productForCache = _productRepository.GetById(id);
            var productMap = _mapper.Map<ProductDto>(productForCache);
            _cacheRepository.SetOrUpdate(id.ToString(), productMap);
            return productMap;
        }

        public ProductDto UpdateProduct(ProductDto productDto)
        {
            var productMap = _mapper.Map<Product>(productDto);
            var productForUpdate = _productRepository.UpdateProduct(productMap);
            productForUpdate= _cacheRepository.SetOrUpdate(productDto.Id.ToString(), productForUpdate);
            
            return _mapper.Map<ProductDto>(productForUpdate);
        }
    }
}
