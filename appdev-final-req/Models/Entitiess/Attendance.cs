namespace appdev_final_req.Models.Entitiess
{
    public class Attendance
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int EventId { get; set; }
        public bool IsPresent { get; set; }

        // for foreign key
        public Member Member { get; set; }
        public Event Event { get; set; }
    }
}
