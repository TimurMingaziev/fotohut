using System.Collections.Generic;
using System.Threading.Tasks;
using Fotohut.API.Models;
using Fotohut.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fotohut.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Contacts>>>> GetContacts()
        {
            var contacts = await _contactsService.GetContactsAsync();
            return Ok(new ApiResponse<IEnumerable<Contacts>>(contacts, "Contacts retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Contacts>>> GetContact(int id)
        {
            var contact = await _contactsService.GetContactByIdAsync(id);

            if (contact == null)
            {
                return NotFound(new ApiResponse<Contacts>("Contact not found"));
            }

            return Ok(new ApiResponse<Contacts>(contact, "Contact retrieved successfully"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Contacts>>> CreateContact([FromBody] Contacts contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<Contacts>("Invalid contact data"));
            }

            var createdContact = await _contactsService.CreateContactAsync(contact);
            return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, new ApiResponse<Contacts>(createdContact, "Contact created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Contacts>>> UpdateContact(int id, [FromBody] Contacts contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<Contacts>("Invalid contact data"));
            }

            var updatedContact = await _contactsService.UpdateContactAsync(id, contact);
            
            if (updatedContact == null)
            {
                return NotFound(new ApiResponse<Contacts>("Contact not found"));
            }

            return Ok(new ApiResponse<Contacts>(updatedContact, "Contact updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteContact(int id)
        {
            var result = await _contactsService.DeleteContactAsync(id);
            
            if (!result)
            {
                return NotFound(new ApiResponse<bool>("Contact not found"));
            }

            return Ok(new ApiResponse<bool>(true, "Contact deleted successfully"));
        }
    }
} 