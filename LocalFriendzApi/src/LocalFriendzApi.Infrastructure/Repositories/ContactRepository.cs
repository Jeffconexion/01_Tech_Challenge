﻿using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;
using LocalFriendzApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LocalFriendzApi.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {

        private readonly ILogger<ContactRepository> _logger;
        private readonly AppDbContext _context;

        public ContactRepository(ILogger<ContactRepository> logger,
                                 AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Response<Contact?>> Create(Contact contact)
        {
            try
            {
                _logger.LogInformation("Create method called for Contact: {ContactName}", contact.Name);

                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Contact created successfully: {ContactId}", contact.IdContact);

                return new Response<Contact?>(contact, 201, message: "Contact created with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a contact.");
                return new Response<Contact?>(contact, 500, message: "Internal server erro.");
            }
        }

        public async Task<Response<Contact?>> Update(Guid idContact, UpdateContactRequest request)
        {
            try
            {
                _logger.LogInformation("Update method called for Contact ID: {ContactId}", idContact);

                var contact = await _context.Contacts
                                        .Where(c => c.IdContact == idContact)
                                        .Include(c => c.AreaCode)
                                        .FirstOrDefaultAsync(c => c.IdContact == idContact);

                if (contact is null)
                {
                    _logger.LogWarning("Contact not found: {ContactId}", idContact);
                    return new Response<Contact?>(null, 404, message: "Contact not found!");
                }

                // Novos valores
                contact.Name = request.Name;
                contact.Email = request.Email;
                contact.Phone = request.Phone;
                contact.AreaCode.CodeRegion = request.AreaCode;

                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Contact updated successfully: {ContactId}", idContact);
                return new Response<Contact?>(contact, message: "Contact update with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the contact with ID: {ContactId}", idContact);
                return new Response<Contact?>(null, 500, message: "Internal Server Erro!");
            }
        }

        public async Task<Response<Contact?>> Delete(Guid idContact)
        {
            try
            {
                _logger.LogInformation("Delete method called for Contact ID: {ContactId}", idContact);

                var contact = await _context.Contacts
                                        .Include(c => c.AreaCode)
                                        .FirstOrDefaultAsync(c => c.IdContact == idContact);

                if (contact is null)
                {
                    _logger.LogWarning("Contact not found: {ContactId}", idContact);
                    return new Response<Contact?>(null, 404, message: "Contact not found!");
                }

                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Contact deleted successfully: {ContactId}", idContact);


                return new Response<Contact?>(contact, message: "Removed contact with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the contact with ID: {ContactId}", idContact);
                return new Response<Contact?>(null, 500, message: "Internal server erro!");
            }
        }

        public async Task<PagedResponse<List<Contact>?>> GetAll(GetAllContactRequest request)
        {
            try
            {
                _logger.LogInformation("GetAll method called. PageNumber: {PageNumber}, PageSize: {PageSize}", request.PageNumber, request.PageSize);

                var query = await _context
                                  .Contacts
                                  .AsNoTracking()
                                  .Include(c => c.AreaCode)
                                  .OrderBy(c => c.Name)
                                  .ToListAsync();

                var contacts = query
                                   .Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToList();

                var count = query.Count();

                if (contacts.Any() is false)
                {
                    _logger.LogInformation("Contacts not found!");
                    return new PagedResponse<List<Contact>?>(null, 404, message: "Not found contact.");
                }

                _logger.LogInformation("GetAll method executed successfully. Total contacts: {Count}", count);

                return new PagedResponse<List<Contact>?>(
                    contacts,
                    count,
                    request.PageNumber,
                    request.PageSize,
                    message: "Contacts found!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all contacts.");
                return new PagedResponse<List<Contact>?>(null, 500, message: "Internal Server Erro!");
            }
        }

        public async Task<PagedResponse<List<Contact>?>> GetContactByFilter(GetByCodeRegionRequest request)
        {
            try
            {
                _logger.LogInformation("GetAll method called. PageNumber: {PageNumber}, PageSize: {PageSize}", request.PageNumber, request.PageSize);

                var query = await _context
                                  .Contacts
                                  .AsNoTracking()
                                  .Where(c => c.AreaCode.CodeRegion.Equals(request.CodeRegion))
                                  .Include(c => c.AreaCode)
                                  .OrderBy(c => c.Name)
                                  .ToListAsync();

                var contacts = query
                                   .Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToList();

                var count = query.Count();

                if (contacts.Any() is false)
                {
                    _logger.LogInformation("Contacts not found!");
                    return new PagedResponse<List<Contact>?>(null, 404, message: "Not found contact.");
                }

                _logger.LogInformation("GetAll method executed successfully. Total contacts: {Count}", count);

                return new PagedResponse<List<Contact>?>(
                    contacts,
                    count,
                    request.PageNumber,
                    request.PageSize,
                    message: "Contacts found!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all contacts.");
                return new PagedResponse<List<Contact>?>(null, 500, message: "Internal Server Erro!");
            }
        }
    }
}
