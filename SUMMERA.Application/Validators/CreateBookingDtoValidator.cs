using FluentValidation;
using SUMMERA.Application.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Validators
{
    public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingDtoValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0);

            RuleFor(x => x.CheckInDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Check-in date cannot be in the past");

            RuleFor(x => x.CheckOutDate)
                .GreaterThan(x => x.CheckInDate)
                .WithMessage("Check-out must be after check-in");

            RuleFor(x => x.NumberOfGuests)
                .GreaterThan(0).LessThanOrEqualTo(10);
        }
    }
}
