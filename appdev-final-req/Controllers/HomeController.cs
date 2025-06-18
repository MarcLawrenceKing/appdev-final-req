using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text;
using appdev_final_req.Data;
using appdev_final_req.Models;
using appdev_final_req.Models.Entitiess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace appdev_final_req.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext dbContext, ILogger<HomeController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            // Converts DateTime.Today (which has time) to DateOnly, which stores just the date.
            var today = DateOnly.FromDateTime(DateTime.Today);

            // Asynchronously fetches all rows from each table (Members, Events, Attendance)
            var members = await dbContext.Members.ToListAsync();
            var events = await dbContext.Events.ToListAsync();
            var attendance = await dbContext.Attendance.ToListAsync();

            // Counts the total number of members.
            // Counts how many members are marked as active (IsActive == true).
            var totalMembers = members.Count;
            var activeMembers = members.Count(m => m.IsActive);

            // Checks if there are any members.
            // If yes, it computes average age using DateOnly logic:
            // today.Year - m.Birthdate.Year gives rough age.
            // Subtracts 1 if the member hasn't had their birthday this year yet.
            // If there are no members, returns 0

            var avgAge = members.Any()
                ? members.Average(m => (today.Year - m.Birthdate.Year - (m.Birthdate > today.AddYears(-(today.Year - m.Birthdate.Year)) ? 1 : 0)))
                : 0;

            // Counts the total number of events.
            // Counts events scheduled after today.
            // Counts events that are before today.
            var totalEvents = events.Count;
            var upcomingEvents = events.Count(e => e.EventDate > today);
            var pastEvents = events.Count(e => e.EventDate < today);

            // Get only past and current events
            var validEvents = events.Where(e => e.EventDate <= today).ToList();

            // Get valid event IDs
            var validEventIds = validEvents.Select(e => e.Id).ToHashSet();

            // Filter attendance for only valid events
            var validAttendance = attendance.Where(a => validEventIds.Contains(a.EventId)).ToList();

            // Assumes each member should attend every event.
            // Computes expected total attendance records
            var totalAttendanceRecords = validEvents.Count * totalMembers;

            // Counts how many members were marked present in the attendance records.
            var totalPresent = validAttendance.Count(a => a.IsPresent);

            // Calculates average number of attendees per event:
            // Groups present attendance by EventId.
            // Averages the group sizes.
            // If there are no events, returns 0.
            // var attendancePerEvent = validEvents.Count > 0
            //     ? validAttendance.Where(a => a.IsPresent).GroupBy(a => a.EventId).Average(g => g.Count())
            //     : 0;

            // pinalitan ko ung above code kasi may error sa validEvents.Count > 0
            // It was trying to access a property on a null object.
            double attendancePerEvent = 0;
            var groupedAttendance = validAttendance
                .Where(a => a.IsPresent)
                .GroupBy(a => a.EventId)
                .ToList();

            if (groupedAttendance.Any())
            {
                attendancePerEvent = groupedAttendance.Average(g => g.Count());
            }

            // Calculates the overall attendance rate:
            // Present count � expected attendance records.
            // Returns 0 if there are no expected records to avoid division by zero.
            var overallAttendanceRate = totalAttendanceRecords > 0
                ? (double)totalPresent / totalAttendanceRecords
                : 0;

            var viewModel = new DashboardViewModel
            {
                today = today,
                TotalMembers = totalMembers,
                ActiveMembers = activeMembers,
                AverageMemberAge = Math.Round(avgAge, 2),

                TotalEvents = totalEvents,
                UpcomingEvents = upcomingEvents,
                PastEvents = pastEvents,

                TotalAttendanceRecords = totalAttendanceRecords,
                AverageAttendancePerEvent = Math.Round(attendancePerEvent, 2),
                OverallAttendanceRate = Math.Round(overallAttendanceRate * 100, 2) // In percent
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
