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
    [Route("api/Users")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MerkleKitchenAPIUser")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserAuthentication model)
        {
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [AllowAnonymous]
        [HttpGet("{userId:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ReadOneAsync(int userId)
        {
            var user = await _userService.ReadOneAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] UserRegistration user)
        {        
            return Ok(await _userService.CreateAsync(user, user.Password));
        }


        [AllowAnonymous]
        [HttpPatch("activateAccount/{userId:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateAccountAsync(int userId, [FromBody] UserUpdateDto user)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }
            await _userService.ActivateAccountAsync(user);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPatch("resetPassword/{userId:int}", Name = "ResetPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPasswordAsync(int userId, [FromBody] UserUpdateDto user)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }
            await _userService.ResetPasswordAsync(user);

            return NoContent();
        }


        [AllowAnonymous]
        [HttpDelete("{userId:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            if (!_userService.UserExists(userId))
            {
                return NotFound();
            }
            await _userService.DeleteAsync(userId);
            return NoContent();
        }


    }
}
