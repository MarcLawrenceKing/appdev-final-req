using appdev_final_req.Data;
using appdev_final_req.Models;
using appdev_final_req.Models.Entitiess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appdev_final_req.Controllers
{
    [Authorize] // Protect the whole controller
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AttendanceController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // lists all events record
        [HttpGet]
        public async Task<IActionResult> List(string search)
        {
            var events = dbContext.Events.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                events = events.Where(e =>
                    e.Title.ToLower().Contains(search.ToLower()) ||
                    e.EventDate.ToString().Contains(search)
                );
            }

            var result = await events.ToListAsync();
            return View(result);
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

            await UpdateMemberActivityStatusAsync();

            return RedirectToAction("List");
        }


        // updates the isActive attribute to true if user is present for 50% of all events
        // First UPDATE: Sets active members.
        // Second UPDATE: Resets everyone else to inactive.
        [HttpPost]
        private async Task UpdateMemberActivityStatusAsync()
        {
            var sql = @"
            DECLARE @TotalEvents INT;
            SELECT @TotalEvents = COUNT(*) FROM Events;

            UPDATE Members
            SET IsActive = 1
            WHERE Id IN (
                SELECT MemberId
                FROM Attendance
                WHERE IsPresent = 1
                GROUP BY MemberId
                HAVING COUNT(*) * 1.0 / NULLIF(@TotalEvents, 0) >= 0.5
            );

            UPDATE Members
            SET IsActive = 0
            WHERE Id NOT IN (
                SELECT MemberId
                FROM Attendance
                WHERE IsPresent = 1
                GROUP BY MemberId
                HAVING COUNT(*) * 1.0 / NULLIF(@TotalEvents, 0) >= 0.5
            );
        ";

            await dbContext.Database.ExecuteSqlRawAsync(sql);
        }
    }
}
