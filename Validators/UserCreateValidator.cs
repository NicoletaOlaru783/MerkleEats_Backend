using FluentValidation;
using MerkleKitchenApp_V2.Model;
using MerkleKitchenApp_V2.Model.Dtos;
using MerkleKitchenApp_V2.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MerkleKitchenApp_V2.Validators
{
    public class UserCreateValidator : AbstractValidator<UserRegistration>
    {
        public UserCreateValidator(IUserService userService)
        {
            CascadeMode = CascadeMode.Stop;            
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();            
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords must match");
            RuleFor(x => x.Password).NotEmpty()
                .Must(userService.IsPasswordValid)
                .WithMessage("'Password' must contain at least one upper case, a lower case a number and a special character")
                .MinimumLength(5).WithMessage("'Password' has to have at least 5 characters");
            RuleFor(x => x.Email).NotEmpty().EmailAddress()
                .Must(email => email.EndsWith("@emea.merkleinc.com"))
                .WithMessage("'Email' must be a valid Merkle address")
                .Must(userService.IsUniqueUser)
                .WithMessage("An account with this email address exists already");
        }
        


    }
}
