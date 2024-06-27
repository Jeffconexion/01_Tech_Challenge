using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;

namespace LocalFriendzApi.Core.IRepositories
{
    public interface IContactRepository
    {
        Task<Response<Contact?>> Create(Contact contact);
        Task<PagedResponse<List<ContactWithAreaCode>?>> GetAll(GetAllContactRequest request);
        Task<Response<Contact?>> Update(Guid id, Contact request);
        Task<Response<Contact?>> Delete(Guid id);
        Task<Response<List<ContactWithAreaCode>?>> GetContactByFilter(int codeRegion);
        Task<Guid?> GetAreaCodeIdByDDD(int codeRegion);

        Task<Response<AreaCode?>> CreateDDD(AreaCode areaCode);
    }

}
