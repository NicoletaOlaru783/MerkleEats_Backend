using FluentValidation;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using MerkleKitchenApp_V2.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MerkleKitchenApp_V2.Validators
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();

        }
    }
}
