using AutoMapper;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using MerkleKitchenApp_V2.Repository.IRepository;
using MerkleKitchenApp_V2.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Controllers
{
    [Authorize]
    [Route("api/Products")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MerkleKitchenAPIProduct")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //Methods
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> ReadAllAsync()
        {
            var products = await _productService.ReadAllAsync();            
            
            return Ok(products);
        }


        [AllowAnonymous]
        [HttpGet("{productId:int}", Name = "GetProduct")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ReadOneAsync(int productId)
        {
            var product = await _productService.ReadOneAsync(productId);
            if (product == null)
            {
                return NotFound();  
            }
            return Ok(product);
        }

        //Create product
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] ProductCreateDto product)
        {
            return Ok(await _productService.CreateAsync(product));
        }


        [AllowAnonymous]
        [HttpPatch("{productId:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int productId, ProductDto product)
        {
            if (!_productService.ProductExists(productId))
            {
                return NotFound();
            }
            await _productService.UpdateAsync(product);

            return NoContent();
        }


        [AllowAnonymous]
        [HttpDelete("{productId:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            if (!_productService.ProductExists(productId))
            {
                return NotFound();
            }
            await _productService.DeleteAsync(productId);
            return NoContent();
        }

    }

   
}
