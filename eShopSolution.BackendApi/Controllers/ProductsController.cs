using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IManageProductService _manageProductService;

        public ProductsController(IManageProductService manageProductService)
        {
            _manageProductService = manageProductService;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var products = new List<eShopSolution.ViewModel.Catalog.Product.ProductViewModel>();
            Console.WriteLine("before");
            _manageProductService.GetAll();
            Console.WriteLine("After");
            return Ok("hello");
        }

        [HttpGet("/Get2")]
        public IActionResult GetAll2()
        {
            var products = _manageProductService.GetAll2();
            return Ok(products);
        }
    }
}
