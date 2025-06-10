using appdev_final_req.Data;
using appdev_final_req.Models.Entitiess;
using appdev_final_req.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appdev_final_req.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EventsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> List()
        {
            var events = await dbContext.Events.ToListAsync();
            return View(events);
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
    }
}
