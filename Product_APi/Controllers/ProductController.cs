using BLL.Contracts;
using BLL.Models;
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
            if (product == null)
                return BadRequest();
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
        public IActionResult CreateProduct(ProductDto productDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var producte =_productRepository.CreateProduct(productDto);
            return Ok(producte);
        }
        [HttpPut]
        [Route("/[controller]/[action]")]
        public IActionResult UpdateProduct(ProductDto productDto)
        {
            var prod = _productRepository.GetById(productDto.Id);
            if (prod == null)
                return BadRequest();
            var producte = _productRepository.UpdateProduct(productDto);
            return Ok(producte);
        }
        [HttpDelete]
        [Route("/[controller]/[action]")]
        public IActionResult DeleteProduct(int id)
        {
            var prod = _productRepository.GetById(id);  
            if(prod==null)
                return BadRequest();
             _productRepository.DeleteProduct(id);
            return Ok("successfuly delete");
        }
    }
}
