using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IProductRepository
    {
        T GetTest<T>(Expression<Func<T,bool>> expression) where T : class;
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product CreateProduct(Product product);
        Product UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
