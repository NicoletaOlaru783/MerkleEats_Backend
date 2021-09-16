using FluentValidation;
using MerkleKitchenApp_V2.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MerkleKitchenApp_V2.Validators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            //CascadeMode = CascadeMode.Stop;
            //RuleFor(x => x.Id).NotEmpty();
            //RuleFor(x => x.FirstName).NotEmpty();
            //RuleFor(x => x.LastName).NotEmpty();
            //RuleFor(x => x.IsActive).NotEmpty();
        }
    }
}
