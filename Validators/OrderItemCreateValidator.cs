using FluentValidation;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Validators
{
    public class OrderItemCreateValidator : AbstractValidator<OrderItemCreateDto>
    {
        public OrderItemCreateValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
