using BuildMaterials.Core.Contracts;
using BuildMaterials.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Core.Services
{
    public class ShoppingCart : IShoppingCart
    {
        public Task<Contact> AddContactAsync(IShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IShoppingCart> GetContactByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IShoppingCart>> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Contact> UpdateContactAsync(IShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
