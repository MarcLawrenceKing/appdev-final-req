using appdev_final_req.Data;
using appdev_final_req.Models;
using appdev_final_req.Models.Entitiess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appdev_final_req.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public MembersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var members = await dbContext.Members.ToListAsync();
            return View(members);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMemberViewModel viewModel)
        {
            var member = new Member
            { FullName = viewModel.FullName,
              Email = viewModel.Email,
              Phone = viewModel.Phone,
              Birthdate = viewModel.Birthdate,
            };
            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var member = await dbContext.Members.FindAsync(id);

            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Member viewModel)
        {
            var member = await dbContext.Members.FindAsync(viewModel.Id);

            if (member is not null)
            {
                member.FullName = viewModel.FullName;
                member.Email = viewModel.Email;
                member.Phone = viewModel.Phone;
                member.Birthdate = viewModel.Birthdate;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)  // Simplified parameter
        {
            var member = await dbContext.Members.FindAsync(id);

            if (member != null)
            {
                dbContext.Members.Remove(member);
                await dbContext.SaveChangesAsync();
                TempData["Message"] = "Member deleted successfully";
            }

            return RedirectToAction("List");
        }
    }
}
