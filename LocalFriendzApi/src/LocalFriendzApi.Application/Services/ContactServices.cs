﻿using LocalFriendzApi.Application.IServices;
using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;

namespace LocalFriendzApi.Application.Services
{
    public class ContactServices : IContactServices
    {
        private readonly IContactRepository? _contactRepository;

        public ContactServices(IContactRepository? contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Response<Contact?>> CreateAsync(CreateContactRequest request)
        {
            var contact = Contact.RequestMapper(request);
            var response = await _contactRepository.Create(contact);

            return response;
        }
        public async Task<PagedResponse<List<Contact>?>> GetAll(GetAllContactRequest request)
        {
            var response = await _contactRepository.GetAll(request);
            return response;
        }

        public async Task<Response<Contact?>> PutContact(Guid id, UpdateContactRequest request)
        {
            var response = await _contactRepository.Update(id, request);
            return response;
        }

        public async Task<Response<Contact?>> DeleteContact(Guid id)
        {
            var response = await _contactRepository.Delete(id);
            return response;
        }

        public async Task<Response<Contact?>> GetByFilter(string codeRegion)
        {
            var response = await _contactRepository.GetContactByFilter(codeRegion);
            return response;
        }

    }
}
