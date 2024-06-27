using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Core.IIntegration;
using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;

namespace LocalFriendzApi.Application.Services
{
    public class ContactServices : IContactServices
    {
        private readonly IContactRepository? _contactRepository;
        private readonly IInfoDDDIntegration? _infoDDDIntegration;

        public ContactServices(IContactRepository? contactRepository, IInfoDDDIntegration infoDDDIntegration)
        {
            _contactRepository = contactRepository;
            _infoDDDIntegration = infoDDDIntegration;
        }

        public async Task<Response<Contact?>> CreateAsync(CreateContactRequest request)
        {
          
                if (await _infoDDDIntegration.GetDDDInfo(request.CodeRegion))
                {
                    if (await EnsureDDDExists(request.CodeRegion) != null)
                    {

                        Contact contact = new();
                        contact.Name = request.Name;
                        contact.Phone = request.Phone;
                        contact.Email = request.Email;
                        contact.AreaCodeId = (Guid)await EnsureDDDExists(request.CodeRegion);

                        var response = await _contactRepository.Create(contact);

                        return response;
                    }
                }

                return null;

            
           
        }



        public async Task<PagedResponse<List<ContactWithAreaCode>?>> GetAll(GetAllContactRequest request)
        {
            var response = await _contactRepository.GetAll(request);
            return response;
        }

        public async Task<Response<Contact?>> PutContact(Guid id, UpdateContactRequest request)
        {

            if (await _infoDDDIntegration.GetDDDInfo(request.CodeRegion))
            {

                var idAreaCode = await EnsureDDDExists(request.CodeRegion);
                if (idAreaCode != null)
                {
                    Contact contact = new();
                    contact.Id = id;
                    contact.Phone = request.Phone;
                    contact.Email = request.Email;
                    contact.Phone = request.Phone;
                    contact.Name = request.Name;
                    contact.AreaCodeId = (Guid)idAreaCode;

                    var response = await _contactRepository.Update(id, contact);
                    return response;
                }
            }

            return null;
        }

        public async Task<Response<Contact?>> DeleteContact(Guid id)
        {
            var response = await _contactRepository.Delete(id);
            return response;
        }

        public async Task<Response<List<ContactWithAreaCode>?>> GetByFilter(int codeRegion)
        {
            var response = await _contactRepository.GetContactByFilter(codeRegion);
            return response;
        }


        private async Task<Guid?> EnsureDDDExists(int codeRegion)
        {
            Guid? idAreaCode = await _contactRepository.GetAreaCodeIdByDDD(codeRegion);
            if (idAreaCode == null)
            {
                AreaCode areaCode = new();
                areaCode.CodeRegion = codeRegion;
                var result = await _contactRepository.CreateDDD(areaCode);
                return result.Data.IdAreaCode;
            }

            return idAreaCode;

        }

    }
}
