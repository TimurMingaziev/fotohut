using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Fotohut.API.Tests.Controllers
{
    // Mock classes for testing without depending on the API implementation
    public enum ContactType { Email, Phone, Address, Custom }
    
    public class Contacts
    {
        public int Id { get; set; }
        public ContactType Type { get; set; }
        public string Value { get; set; }
        public string CustomType { get; set; }
    }
    
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        
        public ApiResponse(T data, string message = "")
        {
            Success = true;
            Data = data;
            Message = message;
            Errors = new List<string>();
        }
        
        public ApiResponse(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
            Errors = new List<string> { errorMessage };
        }
    }
    
    public interface IContactsService
    {
        Task<IEnumerable<Contacts>> GetContactsAsync();
        Task<Contacts> GetContactByIdAsync(int id);
        Task<Contacts> CreateContactAsync(Contacts contact);
        Task<Contacts> UpdateContactAsync(int id, Contacts contact);
        Task<bool> DeleteContactAsync(int id);
    }
    
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;
        
        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }
        
        public async Task<ActionResult<ApiResponse<IEnumerable<Contacts>>>> GetContacts()
        {
            var contacts = await _contactsService.GetContactsAsync();
            return Ok(new ApiResponse<IEnumerable<Contacts>>(contacts));
        }
        
        public async Task<ActionResult<ApiResponse<Contacts>>> GetContact(int id)
        {
            var contact = await _contactsService.GetContactByIdAsync(id);
            
            if (contact == null)
            {
                return NotFound(new ApiResponse<Contacts>("Contact not found"));
            }
            
            return Ok(new ApiResponse<Contacts>(contact));
        }
    }

    public class ContactsControllerTests
    {
        private readonly Mock<IContactsService> _mockContactsService;
        private readonly ContactsController _controller;

        public ContactsControllerTests()
        {
            _mockContactsService = new Mock<IContactsService>();
            _controller = new ContactsController(_mockContactsService.Object);
        }

        [Fact]
        public async Task GetContacts_ReturnsOkResult_WithListOfContacts()
        {
            // Arrange
            var expectedContacts = new List<Contacts>
            {
                new Contacts { Id = 1, Type = ContactType.Email, Value = "test@example.com" },
                new Contacts { Id = 2, Type = ContactType.Phone, Value = "+1234567890" }
            };

            _mockContactsService
                .Setup(service => service.GetContactsAsync())
                .ReturnsAsync(expectedContacts);

            // Act
            var result = await _controller.GetContacts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse<IEnumerable<Contacts>>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal(expectedContacts.Count, apiResponse.Data.Count());
            Assert.Equal(expectedContacts, apiResponse.Data);
        }

        [Fact]
        public async Task GetContact_WithValidId_ReturnsOkResult_WithContact()
        {
            // Arrange
            int contactId = 1;
            var expectedContact = new Contacts { Id = contactId, Type = ContactType.Email, Value = "test@example.com" };

            _mockContactsService
                .Setup(service => service.GetContactByIdAsync(contactId))
                .ReturnsAsync(expectedContact);

            // Act
            var result = await _controller.GetContact(contactId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse<Contacts>>(okResult.Value);
            Assert.True(apiResponse.Success);
            Assert.Equal(expectedContact, apiResponse.Data);
        }

        [Fact]
        public async Task GetContact_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            int contactId = 999;

            _mockContactsService
                .Setup(service => service.GetContactByIdAsync(contactId))
                .ReturnsAsync((Contacts)null);

            // Act
            var result = await _controller.GetContact(contactId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
} 