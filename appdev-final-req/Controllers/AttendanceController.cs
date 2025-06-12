using appdev_final_req.Data;
using appdev_final_req.Models;
using appdev_final_req.Models.Entitiess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace appdev_final_req.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AttendanceController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // lists all events record
        public async Task<IActionResult> List()
        {
            var events = await dbContext.Events.ToListAsync();
            return View(events);
        }

        // lists attendance forms and existing records
        [HttpGet]
        public async Task<IActionResult> MarkAttendance(int id)
        {
            var eventInfo = await dbContext.Events.FindAsync(id);
            if (eventInfo == null) return NotFound();

            var members = await dbContext.Members.ToListAsync();

            // list all existing attendance records for an event
            var existing = await dbContext.Attendance
                .Where(a => a.EventId == id)
                .ToListAsync();

            // this prepares the list of members you will show on the page
            // go through each member, get id and name and compare it with existing list member id, if found, then member isPresent becomes true

            var viewModel = members.Select(m => new AttendanceViewModel
            {
                MemberId = m.Id,
                FullName = m.FullName,
                IsPresent = existing.FirstOrDefault(a => a.MemberId == m.Id)?.IsPresent ?? false
            }).ToList();

            // save the event id and title to be displayed on the html 
            ViewBag.EventId = id;
            ViewBag.EventTitle = eventInfo.Title;

            return View(viewModel);
        }

        // this is the method when an attendance form is submitted, it takes the event id, and the list of updated attendance
        public async Task<IActionResult> MarkAttendance(int eventId, List<AttendanceViewModel> attendanceList)
        {
            // store all existing attendance record in a list
            var existing = await dbContext.Attendance
                .Where(a => a.EventId == eventId)
                .ToListAsync();

            // loop through every item and insert the updated attendance 
            foreach (var item in attendanceList)
            {
                // check if record is already existing, this will prevent duplicates in the db
                var record = existing.FirstOrDefault(a => a.MemberId == item.MemberId);
                
                // if attendance record is already existing, just update the isPresent in the db, else create a new attendance record
                if (record != null)
                {
                    record.IsPresent = item.IsPresent;
                }
                else
                {
                    dbContext.Attendance.Add(new Attendance
                    {
                        MemberId = item.MemberId,
                        EventId = eventId,
                        IsPresent = item.IsPresent
                    });
                }
            }

            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }
    }
}
