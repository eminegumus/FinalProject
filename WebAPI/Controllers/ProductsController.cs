using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //loosly coupled:gevşek bağımlılık
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
                _productService = productService;
        }
        [HttpGet("getall")]
        //public List<Product> Get()
        //{
        //    //return new List<Product>
        //    //{
        //    //    new Product{ ProductId=1, ProductName="Elma"},
        //    //    new Product{ ProductId=2, ProductName="Armut"}
        //    //};

        //    /*
        //    IProductService productService = new ProductManager(new EfProductDal());
        //    //burada IProduct servisim Productmanager a bağımlı oldu. Başka türde bir product geldiğinde sıkıntı yaşarım.
        //    //Buna depenceny chain- bağımlılık zinciri deniyor. Bunu kırmak için coonstructor yapısına ihtiyacımız var.
        //    //** içindeki satırları yoruma alıp consturctor yapısını kullanacağız.loosyly coupled a geçiyoruz.
        //    var result=productService.GetAll();
        //    return result.Data;
        //    */

        //   var result=_productService.GetAll();
        //    return result.Data;


        //}
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategory(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Product product) 
        {
            var result=_productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
