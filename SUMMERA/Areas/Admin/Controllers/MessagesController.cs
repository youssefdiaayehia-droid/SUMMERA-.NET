using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUMMERA.Infrastructure.Data;

namespace SUMMERA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Messages
        public async Task<IActionResult> Index()
        {
            var messages = await _context.ContactMessages
                .Where(m => !m.IsDeleted)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return View(messages);
        }

        // POST: /Admin/Messages/MarkAsRead/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message == null || message.IsDeleted) return NotFound();

            message.IsRead = true;
            message.UpdatedAt = DateTime.UtcNow;
            _context.ContactMessages.Update(message);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Message marked as read.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Messages/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message == null || message.IsDeleted) return NotFound();

            message.IsDeleted = true;
            message.UpdatedAt = DateTime.UtcNow;
            _context.ContactMessages.Update(message);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Message deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}