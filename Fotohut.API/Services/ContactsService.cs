using System.Collections.Generic;
using System.Threading.Tasks;
using Fotohut.API.Database;
using Fotohut.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fotohut.API.Services
{
    public class ContactsService : IContactsService
    {
        private readonly ApiDbContext _context;

        public ContactsService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contacts>> GetContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contacts?> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contacts> CreateContactAsync(Contacts contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contacts?> UpdateContactAsync(int id, Contacts contact)
        {
            var existingContact = await _context.Contacts.FindAsync(id);
            
            if (existingContact == null)
            {
                return null;
            }

            existingContact.Type = contact.Type;
            existingContact.Value = contact.Value;
            existingContact.CustomType = contact.CustomType;

            await _context.SaveChangesAsync();
            return existingContact;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            
            if (contact == null)
            {
                return false;
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 