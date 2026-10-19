using Microsoft.AspNetCore.Mvc;
using SUMMERA.Application.Services.Interfaces;

namespace SUMMERA.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // GET: /Hotels?city=Cairo&stars=5
        public async Task<IActionResult> Index(string? city, int? stars)
        {
            var hotels = await _hotelService.GetAllHotelsAsync();

            // Filter by city
            if (!string.IsNullOrWhiteSpace(city))
            {
                hotels = hotels.Where(h =>
                    h.CityName != null &&
                    h.CityName.Contains(city, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by star rating
            if (stars.HasValue && stars > 0)
            {
                hotels = hotels.Where(h => h.StarRating == stars.Value);
            }

            // Pass search values to view
            ViewBag.City = city;
            ViewBag.Stars = stars;
            ViewBag.ResultCount = hotels.Count();

            return View(hotels);
        }

        // GET: /Hotels/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            if (hotel == null) return NotFound();
            return View(hotel);
        }
    }
}