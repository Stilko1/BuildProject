using BuildMaterials.Core.Contracts;
using BuildMaterials.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Core.Services
{
    public class ContactService : IContactService
    {
        public Task<Contact> AddContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetContactByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Contact> UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
