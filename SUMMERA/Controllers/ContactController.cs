using Microsoft.AspNetCore.Mvc;
using SUMMERA.Application.DTOs.ContactMessages;
using SUMMERA.Application.Services.Interfaces;

namespace SUMMERA.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        // GET: /Contact
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateContactMessageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _contactMessageService.CreateAsync(dto);

            TempData["Success"] = "Thank you! Your message has been sent successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
