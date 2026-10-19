using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SUMMERA.Application.DTOs.Bookings;
using SUMMERA.Application.Services.Interfaces;
using SUMMERA.Infrastructure.Identity;
using SUMMERA.ViewModels;

namespace SUMMERA.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(
            IBookingService bookingService,
            IRoomService roomService,
            UserManager<ApplicationUser> userManager)
        {
            _bookingService = bookingService;
            _roomService = roomService;
            _userManager = userManager;
        }

        // GET: /Booking/Create?roomId=5
        public async Task<IActionResult> Create(int roomId)
        {
            var room = await _roomService.GetRoomByIdAsync(roomId);
            if (room == null) return NotFound();

            var vm = new BookingFormViewModel
            {
                Room = room,
                Booking = new CreateBookingDto
                {
                    RoomId = roomId,
                    CheckInDate = DateTime.Today.AddDays(1),
                    CheckOutDate = DateTime.Today.AddDays(3),
                    NumberOfGuests = 1
                }
            };

            return View(vm);
        }

        // POST: /Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingFormViewModel vm)
        {
            // Reload room for display
            vm.Room = await _roomService.GetRoomByIdAsync(vm.Booking.RoomId);
            if (vm.Room == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            try
            {
                var booking = await _bookingService.CreateBookingAsync(user.Id, vm.Booking);
                return RedirectToAction(nameof(Confirmation), new { id = booking.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        // GET: /Booking/Confirmation/5
        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            // Security: make sure user owns this booking
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Optionally: check booking.UserId == user.Id

            return View(booking);
        }

        // GET: /Booking/MyBookings
        public async Task<IActionResult> MyBookings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var bookings = await _bookingService.GetUserBookingsAsync(user.Id);
            return View(bookings);
        }

        // POST: /Booking/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _bookingService.CancelBookingAsync(id);
                TempData["Success"] = "Booking has been cancelled.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(MyBookings));
        }
    }
}