using Microsoft.AspNetCore.Mvc;
using SUMMERA.Application.Services.Interfaces;

namespace SUMMERA.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHotelService hotelService, ILogger<HomeController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }

        // GET: / or /Home/Index
        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            return View(hotels);
        }

        // GET: /Home/About
        public IActionResult About()
        {
            return View();
        }

        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Home/Error404
        public IActionResult Error404()
        {
            return View();
        }

        // GET: /Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}