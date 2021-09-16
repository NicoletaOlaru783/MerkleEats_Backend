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
    [Route("api/OrderItems")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MerkleKitchenAPIOrderItem")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class OrderItemsController : ControllerBase
    {
        private IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        //Methods
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<OrderItemDto>))]
        public async Task<IActionResult> ReadAllAsync()
        {
            var orders = await _orderItemService.ReadAllAsync();

            return Ok(orders);
        }


        [AllowAnonymous]
        [HttpGet("{orderItemId:int}", Name = "GetOrderItem")]
        [ProducesResponseType(200, Type = typeof(OrderItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ReadOneAsync(int orderItemId)
        {
            var order = await _orderItemService.ReadOneAsync(orderItemId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }


        [AllowAnonymous]
        [HttpGet("[action]/{orderId:int}")]
        [ProducesResponseType(200, Type = typeof(OrderItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ReadOneOrderIdAsync(int orderId)
        {
            var order = await _orderItemService.ReadOneOrderIdAsync(orderId);
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
        public async Task<IActionResult> CreateAsync([FromBody] List<OrderItemCreateDto> orderItem)
        {
            return Ok(await _orderItemService.CreateAsync(orderItem));
        }



        [AllowAnonymous]
        [HttpDelete("{orderItemId:int}", Name = "DeleteOrderItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int orderItemId)
        {
            if (!_orderItemService.OrderItemExists(orderItemId))
            {
                return NotFound();
            }
            await _orderItemService.DeleteAsync(orderItemId);
            return NoContent();
        }

    }

   
}
