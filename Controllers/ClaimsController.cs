using ClaimsPoe.Models;
using ClaimsPoe.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClaimsPoe.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly AuthDbContext dbContext;

        public ClaimsController(AuthDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Roles = "Lecturer")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost]
        public async Task<IActionResult> Add(AddClaimModel claimModel)
        {
            var lecturerId = User.FindFirstValue(ClaimTypes.Email);

            var claim = new Models.Entities.Claim
            {
                Id = claimModel.Id,
                Name = claimModel.Name,
                Description = claimModel.Description,
                HoursWorked = claimModel.Hours,
                Rate = claimModel.Rate,
                Total = claimModel.Total,
                Status = claimModel.Status,
                Document = claimModel.Document,
                LecturerId = lecturerId
            };

            await dbContext.Claims.AddAsync(claim);
            await dbContext.SaveChangesAsync();



            //return View();
            return RedirectToAction("ListClaims");
        }

        [Authorize(Roles = "HR")]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var claims = await dbContext.Claims.ToListAsync();

            return View(claims);
        }

        [Authorize(Roles = "HR")]
        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var claim = await dbContext.Claims.FindAsync(id);

            if (claim != null)
            {
                claim.Status = "Approved";
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("ListClaims");
        }
        [Authorize(Roles = "HR")]
        [HttpGet]
        public async Task<IActionResult> Reject(int id)
        {
            var claim = await dbContext.Claims.FindAsync(id);

            if (claim != null)
            {
                claim.Status = "Rejected";
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("ListClaims");
        }

        [Authorize(Roles = "Lecturer")]
        public IActionResult ListClaims()
        {
            var claims = dbContext.Claims.ToList();
            return View(claims);
        }
    }
}
