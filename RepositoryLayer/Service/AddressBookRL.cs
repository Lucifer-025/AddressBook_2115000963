using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class AddressBookRL : IAddressBookRL
    {
        private readonly AddressContext _dbContext;

        public AddressBookRL(AddressContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AddressEntity> GetAllContacts()
        {
            return _dbContext.AddressBookEntries.ToList();
        }

        public AddressEntity GetContactById(int id)
        {
            return _dbContext.AddressBookEntries.Find(id);
        }

        public AddressEntity AddContact(AddressEntity contact)
        {
            _dbContext.AddressBookEntries.Add(contact);
            _dbContext.SaveChanges();
            return contact;
        }

        public AddressEntity UpdateContact(int id, AddressEntity contact)
        {
            var existingContact = _dbContext.AddressBookEntries.FirstOrDefault(c => c.Id == id);

            if (existingContact == null)
            {
                return null;
            }

            // Update fields
            existingContact.Name = contact.Name;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.Email = contact.Email;
            existingContact.Address = contact.Address;

            _dbContext.AddressBookEntries.Update(existingContact);
            _dbContext.SaveChanges();
            return existingContact;
        }

        public bool DeleteContact(int id)
        {
            var entry = _dbContext.AddressBookEntries.Find(id);
            if (entry == null) return false;

            _dbContext.AddressBookEntries.Remove(entry);
            _dbContext.SaveChanges();
            return true;
        }
    }
}