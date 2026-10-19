using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUMMERA.Infrastructure.Identity;

namespace SUMMERA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            // عشان نعرف مين أدمن ومين عميل
            var userRoles = new Dictionary<string, bool>();
            foreach (var user in users)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                userRoles[user.Id] = isAdmin;
            }

            ViewBag.UserRoles = userRoles;
            ViewBag.CurrentUserId = _userManager.GetUserId(User);

            return View(users);
        }

        // POST: /Admin/Users/ToggleAdmin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleAdmin(string id, bool makeAdmin)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // حماية: مينفعش الأدمن يلغي صلاحية نفسه
            if (user.Id == _userManager.GetUserId(User))
            {
                TempData["Error"] = "You cannot change your own role!";
                return RedirectToAction(nameof(Index));
            }

            // حماية: مينفعش نلمس الحساب الأساسي
            if (user.Email == "admin@summera.com")
            {
                TempData["Error"] = "Cannot change the primary administrator role!";
                return RedirectToAction(nameof(Index));
            }

            if (makeAdmin)
            {
                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    TempData["Success"] = $"{user.Email} is now an Admin.";
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                    TempData["Success"] = $"{user.Email} is now a Customer.";
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}