using BuildMaterials.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMaterials.Core.Contracts
{
    public interface IShoppingCart
    {
        Task<IEnumerable<IShoppingCart>> GetContactsAsync();
        Task<IShoppingCart> GetContactByIdAsync(int id);
        Task<Contact> AddContactAsync(IShoppingCart shoppingCart);
        Task<Contact> UpdateContactAsync(IShoppingCart shoppingCart);
        Task DeleteContactAsync(int id);
    }
}
