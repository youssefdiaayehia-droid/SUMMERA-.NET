using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUMMERA.Infrastructure.Data;
using SUMMERA.Infrastructure.Identity;

namespace SUMMERA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalHotels = await _context.Hotels.CountAsync(h => !h.IsDeleted);
            ViewBag.TotalRooms = await _context.Rooms.CountAsync(r => !r.IsDeleted);
            ViewBag.TotalBookings = await _context.Bookings.CountAsync(b => !b.IsDeleted);
            ViewBag.TotalUsers = await _userManager.Users.CountAsync();

            var recentBookings = await _context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Hotel)
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .ToListAsync();
            return View(recentBookings);
        }
    }
}
