using FluentValidation;
using SUMMERA.Application.DTOs.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Validators
{
    public class CreateHotelDtoValidator : AbstractValidator<CreateHotelDto>
    {
        public CreateHotelDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hotel name is required")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("Please select a city");

            RuleFor(x => x.StarRating)
                .InclusiveBetween(1, 5).WithMessage("Star rating must be between 1 and 5");

            RuleFor(x => x.StartingPrice)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
