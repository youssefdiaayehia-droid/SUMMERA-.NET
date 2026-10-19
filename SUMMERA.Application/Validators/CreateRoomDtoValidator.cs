using FluentValidation;
using SUMMERA.Application.DTOs.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Validators
{
    public class CreateRoomDtoValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            RuleFor(x => x.RoomNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.RoomType)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0);

            RuleFor(x => x.Capacity)
                .GreaterThan(0).LessThanOrEqualTo(10);

            RuleFor(x => x.HotelId)
                .GreaterThan(0);
        }
    }
}
