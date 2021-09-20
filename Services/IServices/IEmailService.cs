using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Services.IServices
{
    public interface IEmailService
    {
        bool SendCustomEmail(Email email);
        bool SendConfirmOrderEmail(string email, string UID, string orderType, List<OrderItemCreateDto> orderItem);
        bool SendOrderEmail(List<EmailMultipleRecipientsDto> recipients, int type);
    }
}
