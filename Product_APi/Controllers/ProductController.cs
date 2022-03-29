using BLL.Contracts;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Product_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository; 
        }

    
        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.GetById(id);   
            return Ok(product);
        }
       
        [HttpGet]
        [Route("/[controller]/[action]")]
        public IActionResult GetAllProduct()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }
        [HttpPost]
        [Route("/[controller]/[action]")]
        public IActionResult CreateProduct(Product product)
        {
            var producte =_productRepository.CreateProduct(product);
            return Ok(producte);
        }
        [HttpPut]
        [Route("/[controller]/[action]")]
        public IActionResult UpdateProduct(Product product)
        {
            var producte = _productRepository.UpdateProduct(product);
            return Ok(producte);
        }
        [HttpDelete]
        [Route("/[controller]/[action]")]
        public IActionResult DeleteProduct(int id)
        {
             _productRepository.DeleteProduct(id);
            return Ok("successfuly delete");
        }
    }
}
