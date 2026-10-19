using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SUMMERA.Application.Services.Interfaces;
using SUMMERA.Infrastructure.Identity;

namespace SUMMERA.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookingService _bookingService;

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            IBookingService bookingService)
        {
            _userManager = userManager;
            _bookingService = bookingService;
        }

        // GET: /Profile
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            return View(user);
        }

        // GET: /Profile/MyBookings
        public async Task<IActionResult> MyBookings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var bookings = await _bookingService.GetUserBookingsAsync(user.Id);
            return View(bookings);
        }
    }
}