using AutoMapper;
using SUMMERA.Application.Common.Interfaces;
using SUMMERA.Application.DTOs.Bookings;
using SUMMERA.Application.Services.Interfaces;
using SUMMERA.Domain.Common;
using SUMMERA.Domain.Entities;
using SUMMERA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookingDto> CreateBookingAsync(string userId, CreateBookingDto dto)
        {
            // 1. تأكد إن الغرفة موجودة
            var room = await _unitOfWork.Rooms.GetAsync(
                r => r.Id == dto.RoomId,
                includeProperties: "Hotel");

            if (room == null)
                throw new Exception("Room not found");

            // 2. تأكد إن الغرفة فاضية في التواريخ دي
            var isAvailable = await IsRoomAvailableAsync(
                dto.RoomId, dto.CheckInDate, dto.CheckOutDate);

            if (!isAvailable)
                throw new Exception("Room is not available for the selected dates");

            // 3. احسب السعر
            var nights = (dto.CheckOutDate - dto.CheckInDate).Days;
            if (nights <= 0)
                throw new Exception("Check-out date must be after check-in date");

            var subtotal = room.PricePerNight * nights;
            var tax = subtotal * SD.TaxRate;
            var total = subtotal + tax;

            // 4. اعمل الحجز
            var booking = new Booking
            {
                BookingReference = GenerateBookingReference(),
                UserId = userId,
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                NumberOfGuests = dto.NumberOfGuests,
                NumberOfNights = nights,
                PricePerNight = room.PricePerNight,
                SubTotal = subtotal,
                TaxAmount = tax,
                TotalPrice = total,
                Status = BookingStatus.Pending,
                PaymentStatus = PaymentStatus.Unpaid,
                SpecialRequests = dto.SpecialRequests
            };

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveAsync();

            // 5. جهّز الـ DTO
            var result = _mapper.Map<BookingDto>(booking);
            result.HotelName = room.Hotel.Name;
            result.RoomType = room.RoomType;

            return result;
        }

        public async Task<IEnumerable<BookingDto>> GetUserBookingsAsync(string userId)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(
                filter: b => b.UserId == userId,
                includeProperties: "Room.Hotel");

            var dtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return dtos;
        }

        public async Task<BookingDto?> GetBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(
                filter: b => b.Id == id,
                includeProperties: "Room.Hotel");

            return booking == null ? null : _mapper.Map<BookingDto>(booking);
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            // في حجز متداخل؟ (CheckIn قبل CheckOut الجديد، و CheckOut بعد CheckIn الجديد)
            var overlapping = await _unitOfWork.Bookings.GetAllAsync(
                filter: b => b.RoomId == roomId
                          && b.Status != BookingStatus.Cancelled
                          && b.CheckInDate < checkOut
                          && b.CheckOutDate > checkIn);

            return !overlapping.Any();
        }

        public async Task CancelBookingAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(b => b.Id == id);
            if (booking == null) throw new Exception("Booking not found");

            booking.Status = BookingStatus.Cancelled;
            booking.CancelledAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveAsync();
        }

        private static string GenerateBookingReference()
        {
            return $"{SD.BookingPrefix}-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}";
        }
    }
}
