using MerkleKitchenApp_V2.Model;
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
    [Route("api/SaveMenu")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MerkleKitchenAPIMenu")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MenuController : ControllerBase
    {
        private IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        //Methods
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Menu>))]
        public async Task<IActionResult> ReadAllAsync()
        {
            var menu = await _menuService.ReadAllAsync();
            return Ok(menu);
        }


        //Create product
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] List<Menu> menu)
        {
            return Ok(await _menuService.CreateAsync(menu));
        }


        [AllowAnonymous]
        [HttpDelete(Name = "DeleteMenu")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(List<Menu> ids)
        {
            await _menuService.DeleteAsync(ids);
            return NoContent();
        }
    }
}
