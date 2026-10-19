using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUMMERA.Application.DTOs.Hotels;
using SUMMERA.Domain.Entities;
using SUMMERA.Infrastructure.Data;

namespace SUMMERA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Hotels
        public async Task<IActionResult> Index()
        {
            var hotels = await _context.Hotels
                .Include(h => h.City)
                .Where(h => !h.IsDeleted)
                .OrderByDescending(h => h.CreatedAt)
                .ToListAsync();

            return View(hotels);
        }

        // GET: /Admin/Hotels/Create
        public async Task<IActionResult> Create()
        {
            await LoadCitiesAsync();
            return View();
        }

        // POST: /Admin/Hotels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHotelDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCitiesAsync();
                return View(dto);
            }

            var hotel = new Hotel
            {
                Name = dto.Name,
                Description = dto.Description,
                Address = dto.Address,
                CityId = dto.CityId,
                StarRating = dto.StarRating,
                StartingPrice = dto.StartingPrice,
                MainImageUrl = dto.MainImageUrl ?? "/images/hotels/default.png",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Hotel created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Hotels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null || hotel.IsDeleted) return NotFound();

            var dto = new CreateHotelDto
            {
                Name = hotel.Name,
                Description = hotel.Description,
                Address = hotel.Address,
                CityId = hotel.CityId,
                StarRating = hotel.StarRating,
                StartingPrice = hotel.StartingPrice,
                MainImageUrl = hotel.MainImageUrl
            };

            ViewBag.HotelId = hotel.Id;
            await LoadCitiesAsync();
            return View(dto);
        }

        // POST: /Admin/Hotels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateHotelDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.HotelId = id;
                await LoadCitiesAsync();
                return View(dto);
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null || hotel.IsDeleted) return NotFound();

            hotel.Name = dto.Name;
            hotel.Description = dto.Description;
            hotel.Address = dto.Address;
            hotel.CityId = dto.CityId;
            hotel.StarRating = dto.StarRating;
            hotel.StartingPrice = dto.StartingPrice;
            hotel.MainImageUrl = dto.MainImageUrl;
            hotel.UpdatedAt = DateTime.UtcNow;

            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Hotel updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Hotels/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null || hotel.IsDeleted) return NotFound();

            // Soft Delete
            hotel.IsDeleted = true;
            hotel.UpdatedAt = DateTime.UtcNow;

            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Hotel deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        // Helper method
        private async Task LoadCitiesAsync()
        {
            ViewBag.Cities = new SelectList(
                await _context.Cities.Where(c => !c.IsDeleted).ToListAsync(),
                "Id", "Name");
        }
    }
}