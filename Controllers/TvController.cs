using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
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
    [Route("api/TV")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MerkleKitchenAPITv")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TvController : ControllerBase
    {
        private ITvService _tvService;

        public TvController(ITvService tvService)
        {
            _tvService = tvService;
        }

        //Methods
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TV>))]
        public async Task<IActionResult> ReadAllAsync()
        {
            var settings = await _tvService.ReadAllAsync();

            return Ok(settings);
        }

        [AllowAnonymous]
        [HttpPatch(Name = "UpdateTVSettings")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(TV tv)
        {
          
            await _tvService.UpdateAsync(tv);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Instagram>))]
        public async Task<IActionResult> GetToken()
        {
            var token = await _tvService.ReadOneAsync(1);

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPatch("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken(Instagram instagram)
        {
            await _tvService.UpdateAsync(instagram);

            return NoContent();
        }


    }
}
