using FluentValidation;
using Models.Contacts;
using System;

namespace ContactsService.Validators
{
    public class ContactEditDtoValidator : AbstractValidator<ContactEditDto>
    {
        public ContactEditDtoValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(r => r.Name)
                .NotEmpty();
            RuleFor(r => r.Surname)
                .NotEmpty();
            RuleFor(r => r.TelephoneNumber)
                .NotEmpty();
            RuleFor(r => r.DateOfBirth).NotEmpty()
                .Must(date => date != default(DateTime))
                .WithMessage("Date of birth is required");
        }
    }
}
