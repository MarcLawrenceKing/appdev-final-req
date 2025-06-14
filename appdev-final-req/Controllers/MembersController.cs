using appdev_final_req.Data;
using appdev_final_req.Models;
using appdev_final_req.Models.Entitiess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;


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

        [HttpGet]
        public IActionResult BatchUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BatchUpload(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    using var reader = new StreamReader(file.OpenReadStream());

                    var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        PrepareHeaderForMatch = args => args.Header.ToLower(),
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        BadDataFound = null,
                    };

                    using var csv = new CsvReader(reader, config);

                    csv.Context.TypeConverterCache.AddConverter<DateOnly>(new DateOnlyConverter());

                    var members = csv.GetRecords<Member>().ToList();

                    dbContext.Members.AddRange(members);
                    await dbContext.SaveChangesAsync();

                    ViewBag.Message = $"{members.Count} members uploaded successfully!";
                    return View();
                }

                ViewBag.Message = "Please upload a valid CSV file.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An error occurred: " + ex.Message;
                return View();
            }
        }





    }
}
