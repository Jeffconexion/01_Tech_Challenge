﻿using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;

namespace LocalFriendzApi.Core.IRepositories
{
    public interface IContactRepository
    {
        Task<Response<Contact?>> Create(Contact contact);
        Task<PagedResponse<List<Contact>?>> GetAll(GetAllContactRequest request);
        Task<PagedResponse<List<Contact>?>> GetContactByFilter(GetByCodeRegionRequest request);
        Task<Response<Contact?>> Update(Guid id, UpdateContactRequest request);
        Task<Response<Contact?>> Delete(Guid id);
    }
}
