namespace LocalFriendzApi.Core.Requests.Contact
{
    public class CreateContactRequest
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int CodeRegion { get; set; }
    }
}
