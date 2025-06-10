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
    }
}
