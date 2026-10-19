using SUMMERA.Application.DTOs.Bookings;
using SUMMERA.Application.DTOs.Rooms;

namespace SUMMERA.ViewModels
{
    public class BookingFormViewModel
    {
        public RoomDto Room { get; set; }
        public CreateBookingDto Booking { get; set; }
    }
}