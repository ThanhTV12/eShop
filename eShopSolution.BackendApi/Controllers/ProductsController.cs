using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products.Services;
using eShopSolution.ViewModel.Catalog.Product;
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

        [HttpGet("paging")]
        public async Task<ActionResult> GetPaging([FromQuery]GetManageProductPagingRequest request)
        {
            var products = await _manageProductService.GetAllPaging(request);
            return Ok(products);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId, languageId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm]ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);
            var productVm = await _manageProductService.GetById(productId, "vi-VN");
            return Ok(productVm);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var productId = await _manageProductService.Update(request);
            var product = await _manageProductService.GetById(request.Id, request.LanguageId);
            return Ok(product);
        }
        
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        
        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _manageProductService.updatePrice(productId, newPrice);
            if (isSuccessful)
                return Ok();

            return BadRequest();
        }
        
    }
}
