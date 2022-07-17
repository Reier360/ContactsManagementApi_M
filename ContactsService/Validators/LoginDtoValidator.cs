using FluentValidation;
using Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsService.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(r => r.Username)
                .EmailAddress()
                .NotEmpty();
            RuleFor(r => r.Password)
                    .NotEmpty();
        }
    }
}
