using System.ComponentModel.DataAnnotations;

namespace ClaimsPoe.ViewModels
{
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
       
        public bool RememberMe { get; set; }
    }
}
