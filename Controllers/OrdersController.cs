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
    [Route("api/Orders")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MerkleKitchenAPIOrder")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //Methods
        [AllowAnonymous]
        [HttpGet]        
        [ProducesResponseType(200, Type = typeof(List<OrderDto>))]
        public async Task<IActionResult> ReadAllAsync()
        {
            var orders = await _orderService.ReadAllAsync();

            return Ok(orders);
        }


        [AllowAnonymous]
        [HttpGet("{orderId:int}", Name = "GetOrder")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ReadOneAsync(int orderId)
        {
            var order = await _orderService.ReadOneAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{createdAt:datetime}")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ReadByDateAsync(DateTime createdAt)
        {
            var order = await _orderService.ReadByDateAsync(createdAt);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }


        //Create product
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] OrderCreateDto order)
        {
            return Ok(await _orderService.CreateAsync(order));
        }


        [AllowAnonymous]
        [HttpPatch(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(List<OrderUpdateDto> order)
        {        

            return Ok(await _orderService.UpdateAsync(order));
        }


        [AllowAnonymous]
        [HttpPatch("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmOrder(string UID)
        {
            return Ok(await _orderService.ConfirmOrder(UID));
        }

        [AllowAnonymous]
        [HttpPatch("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelOrder(string UID)
        {
            return Ok(await _orderService.CancelOrder(UID));
        }


        [AllowAnonymous]
        [HttpDelete("{orderId:int}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int orderId)
        {
            if (!_orderService.OrderExists(orderId))
            {
                return NotFound();
            }
            await _orderService.DeleteAsync(orderId);
            return NoContent();
        }

    }

   
}
