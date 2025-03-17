using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressBookRL
    {
        IEnumerable<AddressEntity> GetAllContacts();
        AddressEntity GetContactById(int id);
        AddressEntity AddContact(AddressEntity contact);
        AddressEntity UpdateContact(int id, AddressEntity contact);
        bool DeleteContact(int id);
    }
}