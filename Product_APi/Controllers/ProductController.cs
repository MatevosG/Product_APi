using BLL.Contracts;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Product_APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService; 
        }

    
        [HttpGet]
        [Route("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return BadRequest();
            return Ok(product);
        }
       
        [HttpGet]
        [Route("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }
        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var producte =_productService.CreateProduct(productDto);
            return Ok(producte);
        }
        [HttpPut]
        [Route("UpdateProduct")]
        public IActionResult UpdateProduct(ProductDto productDto)
        {
            var prod = _productService.GetById(productDto.Id);
            if (prod == null)
                return BadRequest();
            var producte = _productService.UpdateProduct(productDto);
            return Ok(producte);
        }
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var prod = _productService.GetById(id);  
            if(prod==null)
                return BadRequest();
             _productService.DeleteProduct(id);
            return Ok("successfuly delete");
        }
    }
}
