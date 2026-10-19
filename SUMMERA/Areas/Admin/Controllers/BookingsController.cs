using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUMMERA.Domain.Entities;
using SUMMERA.Domain.Enums;
using SUMMERA.Infrastructure.Data;
using SUMMERA.Infrastructure.Identity;

namespace SUMMERA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Admin/Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Room)
                    .ThenInclude(r => r.Hotel)
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            // Get user emails for display
            ViewBag.UserEmails = new Dictionary<string, string>();
            foreach (var booking in bookings)
            {
                if (!string.IsNullOrEmpty(booking.UserId) && !ViewBag.UserEmails.ContainsKey(booking.UserId))
                {
                    var user = await _userManager.FindByIdAsync(booking.UserId);
                    if (user != null)
                    {
                        ViewBag.UserEmails[booking.UserId] = user.Email;
                    }
                }
            }

            return View(bookings);
        }

        // POST: /Admin/Bookings/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, BookingStatus status)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null || booking.IsDeleted)
                return NotFound();

            booking.Status = status;
            booking.UpdatedAt = DateTime.UtcNow;

            if (status == BookingStatus.Confirmed)
            {
                booking.ConfirmedAt = DateTime.UtcNow;
                booking.PaymentStatus = PaymentStatus.Paid; // افتراضي
            }
            else if (status == BookingStatus.Cancelled)
            {
                booking.CancelledAt = DateTime.UtcNow;
                booking.PaymentStatus = PaymentStatus.Refunded;
            }

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Booking status updated to {status}.";
            return RedirectToAction(nameof(Index));
        }
    }
}