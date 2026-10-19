using FluentValidation;
using SUMMERA.Application.DTOs.ContactMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Validators
{
    public class CreateContactMessageDtoValidator : AbstractValidator<CreateContactMessageDto>
    {
        public CreateContactMessageDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Subject)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
