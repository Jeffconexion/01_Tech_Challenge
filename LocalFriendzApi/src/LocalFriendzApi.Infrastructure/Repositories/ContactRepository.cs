using LocalFriendzApi.Core.IRepositories;
using LocalFriendzApi.Core.Models;
using LocalFriendzApi.Core.Requests.Contact;
using LocalFriendzApi.Core.Responses;
using LocalFriendzApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

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

                _logger.LogInformation("Contact created successfully: {ContactId}", contact.Id);

                return new Response<Contact?>(contact, 201, "Contact created with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a contact.");
                return new Response<Contact?>(null, 500, "Internal server erro.");
            }
        }


 
        public async Task<Response<Contact?>> Update(Guid id, Contact contact)
        {
            try
            {
                _logger.LogInformation("Update method called for Contact ID: {ContactId}", id);


                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Contact updated successfully: {ContactId}", id);
                return new Response<Contact?>(contact, message: "Contact update with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the contact with ID: {ContactId}", id);
                return new Response<Contact?>(null, 500, "Internal Server Erro!");
            }
        }

        public async Task<Response<Contact?>> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation("Delete method called for Contact ID: {ContactId}", id);

                var contact = await _context.Contacts
                                        .Include(a => a.AreaCode)
                                        .FirstOrDefaultAsync(a => a.Id == id);

                if (contact is null)
                {
                    _logger.LogWarning("Contact not found: {ContactId}", id);
                    return new Response<Contact?>(null, 404, "Contact not found!");
                }

                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Contact deleted successfully: {ContactId}", id);


                return new Response<Contact?>(contact, message: "Removed contact with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the contact with ID: {ContactId}", id);
                return new Response<Contact?>(null, 500, "Internal server erro!");
            }
        }

        public async Task<PagedResponse<List<ContactWithAreaCode>?>> GetAll(GetAllContactRequest request)
        {
            try
            {


                _logger.LogInformation("GetAll method called. PageNumber: {PageNumber}, PageSize: {PageSize}", request.PageNumber, request.PageSize);
                var query = await _context.Contacts.Join(_context.AreasCode,
                                                            contact => contact.AreaCodeId,
                                                            areaCode => areaCode.IdAreaCode,
                                                         (contact, areaCode) => new { Contact = contact, AreaCode = areaCode })
           .Select(result => new ContactWithAreaCode
           {
               Name = result.Contact.Name,
               Phone = result.Contact.Phone,
               Email = result.Contact.Email,
               CodeRegion = result.AreaCode.CodeRegion  // Map CodeRegion correctly from AreaCode
           })
           .ToListAsync();



                var contacts = query
                                   .Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .ToList();

                var count = query.Count();

                _logger.LogInformation("GetAll method executed successfully. Total contacts: {Count}", count);
              
                return new PagedResponse<List<ContactWithAreaCode>?>(
                    contacts,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all contacts.");
                return new PagedResponse<List<ContactWithAreaCode>?>(null, 500, "Internal Server Erro!");
            }
        }


        public async Task<Guid?> GetAreaCodeIdByDDD(int codeRegion)
        {
            try
            {
                _logger.LogInformation("GetAreaCodeIdByDDD method called for DDD: {DDD}", codeRegion);
                var areaCodeId = await _context.AreasCode
                              .Where(x => x.CodeRegion == codeRegion)
                              .Select(x => (Guid?)x.IdAreaCode)
                              .FirstOrDefaultAsync();


                if (areaCodeId == null)
                {
                    _logger.LogWarning("Area code not found for DDD: {DDD}", codeRegion);
                    return null; // Retorna null se não encontrar o código de área
                }

                _logger.LogInformation("Area code found for DDD: {DDD}. AreaCodeId: {AreaCodeId}", codeRegion, areaCodeId);

                return areaCodeId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving AreaCodeId for DDD: {DDD}", codeRegion);
                throw; // Relança a exceção para ser tratada externamente
            }
        }



        public async Task<Response<AreaCode?>> CreateDDD(AreaCode areaCode)
        {

            try
            {
                _logger.LogInformation($"Create method called for area: {areaCode.CodeRegion}");

                await _context.AreasCode.AddAsync(areaCode);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Contact created successfully: {areaCode}", areaCode.IdAreaCode);

                return new Response<AreaCode?>(areaCode, 201, "Area Code created with sucess!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a Area Code.");
                return new Response<AreaCode?>(null, 500, "Internal server erro.");
            }

        }


        public async Task<Response<List<ContactWithAreaCode>?>> GetContactByFilter(int codeRegion)
        {
            try
            {
                _logger.LogInformation("GetContactByFilter method called with CodeRegion: {CodeRegion}", codeRegion);

          

                var contacts = await _context.Contacts
                    .Join(_context.AreasCode,
                        contact => contact.AreaCodeId,
                        areaCode => areaCode.IdAreaCode,
                        (contact, areaCode) => new { Contact = contact, AreaCode = areaCode })
                    .Where(result => result.AreaCode.CodeRegion == codeRegion)
                    .Select(result => new ContactWithAreaCode
                    {
                        Name = result.Contact.Name,
                        Phone = result.Contact.Phone,
                        Email = result.Contact.Email,
                        CodeRegion = result.AreaCode.CodeRegion
                    })
                    .ToListAsync();



                if (contacts.Count == 0)
                {
                    _logger.LogWarning("No contact found with CodeRegion: {CodeRegion}", codeRegion);
                    return new Response<List<ContactWithAreaCode>?>(null, 404, "No contacts found with the specified code region.");
                }

                _logger.LogInformation($"Contacts found with CodeRegion: {codeRegion}");

                return new Response<List<ContactWithAreaCode>?>(contacts, 200, "Contacts found");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting contact with CodeRegion: {CodeRegion}", codeRegion);
                return new Response<List<ContactWithAreaCode>?>(null, 500, "Internal server erro");
            }
        }


    }
}
