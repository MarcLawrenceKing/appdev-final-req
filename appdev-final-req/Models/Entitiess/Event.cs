namespace appdev_final_req.Models.Entitiess
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly EventDate { get; set; }

        // for foreign key

        public ICollection<Attendance> Attendances { get; set; }
    }
}
