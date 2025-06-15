using appdev_final_req.Data;
using appdev_final_req.Models.Entitiess;
using appdev_final_req.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace appdev_final_req.Controllers
{
    [Authorize] // Protect the whole controller
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EventsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> List(string search)
        {
            var events = dbContext.Events.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                events = events.Where(e =>
                    e.Title.ToLower().Contains(search.ToLower()) ||
                    e.Description.ToLower().Contains(search.ToLower()) ||
                    e.EventDate.ToString().Contains(search)
                );
            }

            var result = await events.ToListAsync();
            return View(result);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel viewModel)
        {
            var eventz = new Event
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                EventDate = viewModel.EventDate,

            };
            await dbContext.Events.AddAsync(eventz);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eventz = await dbContext.Events.FindAsync(id);

            return View(eventz);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Event viewModel)
        {
            var eventz = await dbContext.Events.FindAsync(viewModel.Id);

            if (eventz is not null)
            {
                eventz.Title = viewModel.Title;
                eventz.Description = viewModel.Description;
                eventz.EventDate = viewModel.EventDate;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)  // Simplified parameter
        {
            var eventz = await dbContext.Events.FindAsync(id);

            if (eventz != null)
            {
                dbContext.Events.Remove(eventz);
                await dbContext.SaveChangesAsync();
                TempData["Message"] = "Event deleted successfully";
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

                    var events = csv.GetRecords<Event>().ToList();

                    dbContext.Events.AddRange(events);
                    await dbContext.SaveChangesAsync();

                    ViewBag.Message = $"{events.Count} events added successfully!";
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
