using System.ComponentModel.DataAnnotations;

namespace LocalFriendzApi.Core.Requests.Contact
{
    public class CreateContactRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$",
            ErrorMessage = "Invalid phone number format")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "CodeRegion is required")]
        public int CodeRegion { get; set; }
    }

}
