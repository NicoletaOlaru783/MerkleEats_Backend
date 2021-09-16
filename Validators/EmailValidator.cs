using FluentValidation;
using MerkleKitchenApp_V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerkleKitchenApp_V2.Validators
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Subject).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
