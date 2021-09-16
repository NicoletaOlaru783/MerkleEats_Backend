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
    [Route("api/SendEmail")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MerkleKitchenAPIEmail")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class EmailController : ControllerBase
    {
        private IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SendCustomEmail([FromBody] Email email)
        {
            await _emailService.SendCustomEmail(email);
            return Ok();
        }


        [AllowAnonymous]
        [HttpPost("[action]/{type:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SendOrderEmail([FromBody] List<EmailMultipleRecipientsDto> recipients, int type)
        {
            await _emailService.SendOrderEmail(recipients, type);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  bool SendConfirmOrderEmail([FromBody] string email, string UID)
        {
             _emailService.SendConfirmOrderEmail(email, UID);
            return true;
        }



    }
}
