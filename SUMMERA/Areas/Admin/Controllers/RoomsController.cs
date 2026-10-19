using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUMMERA.Application.DTOs.Rooms;
using SUMMERA.Domain.Entities;
using SUMMERA.Infrastructure.Data;

namespace SUMMERA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Rooms
        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Hotel)
                .Where(r => !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return View(rooms);
        }

        // GET: /Admin/Rooms/Create
        public async Task<IActionResult> Create()
        {
            await LoadHotelsAsync();
            return View();
        }

        // POST: /Admin/Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoomDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadHotelsAsync();
                return View(dto);
            }

            var room = new Room
            {
                HotelId = dto.HotelId,
                RoomNumber = dto.RoomNumber,
                RoomType = dto.RoomType,
                Description = dto.Description,
                PricePerNight = dto.PricePerNight,
                Capacity = dto.Capacity,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Room created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Rooms/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null || room.IsDeleted) return NotFound();

            var dto = new CreateRoomDto
            {
                HotelId = room.HotelId,
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomType,
                Description = room.Description,
                PricePerNight = room.PricePerNight,
                Capacity = room.Capacity
            };

            ViewBag.RoomId = room.Id;
            await LoadHotelsAsync();
            return View(dto);
        }

        // POST: /Admin/Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateRoomDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoomId = id;
                await LoadHotelsAsync();
                return View(dto);
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null || room.IsDeleted) return NotFound();

            room.HotelId = dto.HotelId;
            room.RoomNumber = dto.RoomNumber;
            room.RoomType = dto.RoomType;
            room.Description = dto.Description;
            room.PricePerNight = dto.PricePerNight;
            room.Capacity = dto.Capacity;
            room.UpdatedAt = DateTime.UtcNow;

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Room updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Rooms/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null || room.IsDeleted) return NotFound();

            room.IsDeleted = true;
            room.UpdatedAt = DateTime.UtcNow;

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Room deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadHotelsAsync()
        {
            ViewBag.Hotels = new SelectList(
                await _context.Hotels.Where(h => !h.IsDeleted).ToListAsync(),
                "Id", "Name");
        }
    }
}