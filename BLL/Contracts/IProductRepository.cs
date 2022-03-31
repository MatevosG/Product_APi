using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product CreateProduct(ProductDto productDto);
        Product UpdateProduct(ProductDto productDto);
        void DeleteProduct(int id);
    }
}
