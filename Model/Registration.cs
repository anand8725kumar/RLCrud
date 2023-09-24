using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Model
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int IsActive { get; set; }
        public string Role { get; set; }
        [DefaultValue("default_value")]
        public string UserMessage { get; set; }
        [DefaultValue("default_value")]
        public string AccessToken { get; set; }
    }
}
