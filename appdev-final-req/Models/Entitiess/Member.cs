namespace appdev_final_req.Models.Entitiess
{
    public class Member
    {
        public int Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly Birthdate { get; set; }
        public bool IsActive { get; set; }
        public DateOnly DateJoined { get; set; }
    }
}
