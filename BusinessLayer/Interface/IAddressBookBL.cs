
using ModelLayer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBookBL
    {
        Task<IEnumerable<AddressEntry>> GetAllContactsAsync();
        Task<AddressEntry> GetContactByIdAsync(int id);
        Task<AddressEntry> AddContactAsync(RequestModel request);
        Task<AddressEntry> UpdateContactAsync(int id, RequestModel request);
        Task<bool> DeleteContactAsync(int id);
    }
}