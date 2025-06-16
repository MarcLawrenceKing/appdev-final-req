namespace appdev_final_req.Models
{
    public class DashboardViewModel
    {
        // Members KPIs
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int InactiveMembers { get; set; }
        public int NewMembersThisMonth { get; set; }
        public int EngagedMembersOver50Percent { get; set; }
        public double ActiveMemberPercentage => TotalMembers == 0 ? 0 : (double)ActiveMembers / TotalMembers * 100;

        // Events KPIs
        public int TotalEvents { get; set; }
        public int EventsThisMonth { get; set; }
        public double AverageAttendancePerEvent { get; set; }
        public string MostAttendedEventTitle { get; set; }
        public string LowestAttendedEventTitle { get; set; }
        public double NoShowRate { get; set; }

        // Attendance KPIs
        public int TotalAttendanceRecords { get; set; }
        public double MonthlyAttendanceRate { get; set; }
        public int LowParticipationMembers { get; set; }
        public double AverageAttendanceRate { get; set; }
    }
}
