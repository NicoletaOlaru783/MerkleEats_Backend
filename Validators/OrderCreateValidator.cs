using FluentValidation;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Validators
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.DeliveryType).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
