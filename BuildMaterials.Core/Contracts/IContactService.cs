using BuildMaterials.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Core.Contracts
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task<Contact> AddContactAsync(Contact contact);
        Task<Contact> UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
    }
}
