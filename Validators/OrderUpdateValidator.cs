using FluentValidation;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Validators
{
    public class OrderUpdateValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
