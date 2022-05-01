using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int id);
        ProductDto CreateProduct(ProductDto productDto);
        ProductDto UpdateProduct(ProductDto productDto);
        void DeleteProduct(int id);
    }
}
