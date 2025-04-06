using System.Collections.Generic;
using System.Threading.Tasks;
using Fotohut.API.Models;

namespace Fotohut.API.Services
{
    public interface IContactsService
    {
        Task<IEnumerable<Contacts>> GetContactsAsync();
        Task<Contacts?> GetContactByIdAsync(int id);
        Task<Contacts> CreateContactAsync(Contacts contact);
        Task<Contacts?> UpdateContactAsync(int id, Contacts contact);
        Task<bool> DeleteContactAsync(int id);
    }
} 