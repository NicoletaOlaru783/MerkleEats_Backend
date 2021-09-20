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
        public bool SendCustomEmail([FromBody] Email email)
        {
            _emailService.SendCustomEmail(email);
            return true;
        }


        [AllowAnonymous]
        [HttpPost("[action]/{type:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public bool SendOrderEmail([FromBody] List<EmailMultipleRecipientsDto> recipients, int type)
        {
            _emailService.SendOrderEmail(recipients, type);
            return true;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public bool SendConfirmOrderEmail(string email, string UID, string orderType, [FromBody] List<OrderItemCreateDto> orderItem)
        {
             _emailService.SendConfirmOrderEmail(email, UID, orderType, orderItem);
            return true;
        }



    }
}
