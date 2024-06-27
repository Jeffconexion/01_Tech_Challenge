using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;

namespace LocalFriendzApi.Application.IServices
{
    public interface IContactServices
    {
        Task<Response<Contact?>> CreateAsync(CreateContactRequest request);
        Task<PagedResponse<List<ContactWithAreaCode>?>> GetAll(GetAllContactRequest request);
        Task<Response<Contact?>> PutContact(Guid id, UpdateContactRequest request);
        Task<Response<Contact?>> DeleteContact(Guid id);
        Task<Response<List<ContactWithAreaCode>?>> GetByFilter(int codeRegion);
    }
}
