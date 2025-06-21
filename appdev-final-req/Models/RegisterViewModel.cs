using System.ComponentModel.DataAnnotations;

namespace appdev_final_req.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
