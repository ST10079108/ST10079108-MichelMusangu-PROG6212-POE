using System.ComponentModel.DataAnnotations;

namespace ClaimsPoe.Models
{
    public class EditLecturerViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}