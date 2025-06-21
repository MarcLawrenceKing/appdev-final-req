namespace appdev_final_req.Models
{
    public class DashboardViewModel
    {
        public DateOnly today { get; set; }
        // Members KPIs
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public double AverageMemberAge { get; set; }

        // Events KPIs
        public int TotalEvents { get; set; }
        public int UpcomingEvents { get; set; }
        public int PastEvents { get; set; }

        // Attendance KPIs
        public int TotalAttendanceRecords { get; set; }
        public double AverageAttendancePerEvent { get; set; }
        public double OverallAttendanceRate { get; set; }
    }
}
