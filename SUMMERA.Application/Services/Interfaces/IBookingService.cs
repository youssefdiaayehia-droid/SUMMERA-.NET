using SUMMERA.Application.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(string userId, CreateBookingDto dto);
        Task<IEnumerable<BookingDto>> GetUserBookingsAsync(string userId);
        Task<BookingDto?> GetBookingByIdAsync(int id);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
        Task CancelBookingAsync(int id);
    }
}
