using static System.Net.Mime.MediaTypeNames;

namespace appdev_final_req.Models
{
    public class AddEventViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly EventDate { get; set; }

    }
}
