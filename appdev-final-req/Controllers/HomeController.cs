using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            // now: Gets the current date and time.
            // startOfMonth: Sets a date to the first day of the current month(e.g., June 1, 2025).
            var now = DateTime.Now;
            var startOfMonth = new DateOnly(now.Year, now.Month, 1);

            // Fetches all members, and events and their related attendance records using Include.
            var members = await dbContext.Members.ToListAsync();
            var events = await dbContext.Events.Include(e => e.Attendances).ToListAsync();

            // Fetches all attendance rows 
            var attendances = await dbContext.Attendance
                .Include(a => a.Event) // Include Event so you can access Event.Date (join)
                .ToListAsync();

            // Total members
            // Members marked as active
            // Members added this month(assuming CreatedAt is a valid date field)
            int totalMembers = members.Count;
            int activeMembers = members.Count(m => m.IsActive);
            int newMembersThisMonth = members.Count(m => m.DateJoined >= startOfMonth);

            // Event totals and number of attendance records
            int totalEvents = events.Count;
            int eventsThisMonth = events.Count(e => e.EventDate >= startOfMonth);
            int totalAttendanceRecords = attendances.Count;

            // Calculates average attendees per event
            // Finds the event with the most and least attendance
            var averageAttendancePerEvent = totalEvents == 0 ? 0 : events.Average(e => e.Attendances.Count);
            var mostAttendedEvent = events.OrderByDescending(e => e.Attendances.Count).FirstOrDefault();
            var leastAttendedEvent = events.OrderBy(e => e.Attendances.Count).FirstOrDefault();

            // Percentage of absentees across all attendance records
            double noShowRate = totalAttendanceRecords == 0
                ? 0
                : (double)attendances.Count(a => !a.IsPresent) / totalAttendanceRecords * 100;

            // Gets attendance records for the current month 
            var monthlyAttendances = attendances.Where(a => a.Event.EventDate >= startOfMonth).ToList();

            // Calculates the total possible attendance = total members × total monthly events (assuming everyone is expected to attend each)
            var totalPossible = events
                .Where(e => e.EventDate >= startOfMonth)
                .Sum(e => members.Count); // assuming all members are expected

            // The actual attendance rate for this month
            double monthlyAttendanceRate = totalPossible == 0 ? 0 :
                (double)monthlyAttendances.Count(a => a.IsPresent) / totalPossible * 100;

            // Members with >50% attendance (engaged)
            int engagedMembers = members.Count(m =>
            {
                var memberAttendances = attendances.Where(a => a.MemberId == m.Id).ToList();
                int attended = memberAttendances.Count(a => a.IsPresent);
                return memberAttendances.Count > 0 && ((double)attended / memberAttendances.Count) > 0.5;
            });

            // Members with <50% attendance (low participation)
            int lowParticipation = members.Count(m =>
            {
                var memberAttendances = attendances.Where(a => a.MemberId == m.Id).ToList();
                int attended = memberAttendances.Count(a => a.IsPresent);
                return memberAttendances.Count > 0 && ((double)attended / memberAttendances.Count) < 0.5;
            });

            // Calculates the average attendance rate per event, then averages those rates
            double averageAttendanceRate = events.Count == 0 ? 0 :
                events.Average(e =>
                {
                    int count = e.Attendances.Count;
                    int present = e.Attendances.Count(a => a.IsPresent);
                    return count == 0 ? 0 : (double)present / count * 100;
                });

            var viewModel = new DashboardViewModel
            {
                TotalMembers = totalMembers,
                ActiveMembers = activeMembers,
                InactiveMembers = totalMembers - activeMembers,
                NewMembersThisMonth = newMembersThisMonth,
                EngagedMembersOver50Percent = engagedMembers,

                TotalEvents = totalEvents,
                EventsThisMonth = eventsThisMonth,
                AverageAttendancePerEvent = Math.Round(averageAttendancePerEvent, 1),
                MostAttendedEventTitle = mostAttendedEvent?.Title ?? "N/A",
                LowestAttendedEventTitle = leastAttendedEvent?.Title ?? "N/A",
                NoShowRate = Math.Round(noShowRate, 1),

                TotalAttendanceRecords = totalAttendanceRecords,
                MonthlyAttendanceRate = Math.Round(monthlyAttendanceRate, 1),
                LowParticipationMembers = lowParticipation,
                AverageAttendanceRate = Math.Round(averageAttendanceRate, 1)
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
