using ClaimsPoe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ClaimsPoe.Controllers
{
    public class HRController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HRController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [Authorize(Roles = "HR")]
        public async Task<IActionResult> ViewLecturers()
        {
            var lecturers = await userManager.GetUsersInRoleAsync("Lecturer");
            return View(lecturers);
        }

        [HttpGet]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> EditLecturer(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditLecturerViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> EditLecturer(EditLecturerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = model.Email;
            user.UserName = model.UserName;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ViewLecturers));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}
