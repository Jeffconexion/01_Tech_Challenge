using LocalFriendzApi.Core.Requests.Contact;

namespace LocalFriendzApi.Core.Models
{
    public class Contact : BaseModel
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

  
        public Guid AreaCodeId { get; set;  }
        public AreaCode? AreaCode { get; set; }

        public static Contact RequestMapper(CreateContactRequest request)
        {
            return new Contact()
            {
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                AreaCode = new AreaCode() { CodeRegion = request.CodeRegion }
            };
        }
    }
}
