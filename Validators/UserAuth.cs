using FluentValidation;
using MerkleKitchenApp_V2.Model.Dtos;
using MerkleKitchenApp_V2.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Validators
{
    public class UserAuth : AbstractValidator<UserAuthentication>
    {
        public UserAuth(IUserService userService)
        {
            RuleFor(x => x.Username).Must(userService.IsActiveUser).WithMessage("This account is not active");
        }
    }
}
