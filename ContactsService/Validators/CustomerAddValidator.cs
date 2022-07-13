using FluentValidation;
using Models.Contacts;
using System;

namespace ContactsService.Validators
{
    public class CustomerAddValidator : AbstractValidator<ContactAddDto>
    {
        public CustomerAddValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty();
            RuleFor(r => r.Surname)
                .NotEmpty();
            RuleFor(r => r.TelephoneNumber)
                .NotEmpty();
            RuleFor(r => r.EmailAddress)
               .NotEmpty()
               .WithMessage("Email address is required")
               .EmailAddress()
               .WithMessage("A valid email is required");
            RuleFor(r => r.DateOfBirth)
                .NotEmpty()
                .Must(date => date != default(DateTime))
                .WithMessage("Date of birth is required");
        }
    }
}
