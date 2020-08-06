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
        public async Task<IActionResult> GetAll() 
        {
            var products = await _manageProductService.GetAll();
            return Ok(products);
        }
    }
}
