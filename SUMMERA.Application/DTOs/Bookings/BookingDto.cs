using SUMMERA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.DTOs.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string BookingReference { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfNights { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string HotelName { get; set; }
        public string RoomType { get; set; }
    }
}
