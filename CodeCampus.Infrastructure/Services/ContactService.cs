using CodeCampus.Infrastructure.DTOs;
using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Factories;
using CodeCampus.Infrastructure.Interfaces.Repositories;
using CodeCampus.Infrastructure.Interfaces.Services;
using CodeCampus.Infrastructure.Responses;
using Microsoft.Extensions.Logging;

namespace CodeCampus.Infrastructure.Services;

public class ContactService(IContactRepository contactRepository, ILogger<ContactService> logger) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;
    private readonly ILogger<ContactService> _logger = logger;

    public async Task<ResponseResult> SubmitContactRequestAsync(ContactRequestDto request, string? userId)
    {
        try
        {
            var contactEntity = ContactFactory.Create(request, userId);
            return await _contactRepository.CreateOneAsync(contactEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting contact request");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> GetAllContactsAsync()
    {
        try
        {
            var result = await _contactRepository.GetAllAsync();
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = result.Status, Message = result.Message };
            }
            var contacEntities = (IEnumerable<ContactEntity>)result.ContentResult!;
            var contactModels = contacEntities.Select(entity => ContactFactory.Create(entity)).ToList();

            return new ResponseResult { Status = StatusCode.OK, ContentResult = contactModels };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all contacts");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> GetContactByIdAsync(int id)
    {
        try
        {
            var result = await _contactRepository.GetOneAsync(c => c.Id == id);
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.NOT_FOUND, Message = "Contact not found." };
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contact by id");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }

    public async Task<ResponseResult> DeleteContactRequestAsync(int id)
    {
        try
        {
            var result = await _contactRepository.DeleteOneAsync(c => c.Id == id);
            if (result.Status != StatusCode.OK)
            {
                return new ResponseResult { Status = StatusCode.NOT_FOUND, Message = "Contact not found." };
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact request");
            return new ResponseResult { Status = StatusCode.INTERNAL_SERVER_ERROR, Message = ex.Message };
        }
    }
}
