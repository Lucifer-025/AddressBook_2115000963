using AutoMapper;
using BusinessLayer.Interfaces;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Microsoft.Extensions.Logging;
using ModelLayer.Model;

namespace BusinessLayer.Services
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly AddressContext _context;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AddressBookBL(AddressContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressEntry>> GetAllContactsAsync()
        {
            try
            {
                var contacts = _context.AddressBookEntries.ToList();
                return _mapper.Map<IEnumerable<AddressEntry>>(contacts);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching all contacts");
                throw;
            }
        }

        public async Task<AddressEntry> GetContactByIdAsync(int id)
        {
            try
            {
                var contact = _context.AddressBookEntries.FirstOrDefault(e => e.Id == id);
                return _mapper.Map<AddressEntry>(contact);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error fetching contact with ID: {id}");
                throw;
            }
        }

        public async Task<AddressEntry> AddContactAsync(RequestModel request)
        {
            try
            {
                var newContact = _mapper.Map<AddressEntity>(request);
                _context.AddressBookEntries.Add(newContact);
                await _context.SaveChangesAsync();
                return _mapper.Map<AddressEntry>(newContact);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddContactAsync: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }



        public async Task<AddressEntry> UpdateContactAsync(int id, RequestModel request)
        {
            try
            {
                var existingContact = _context.AddressBookEntries.FirstOrDefault(e => e.Id == id);
                if (existingContact == null)
                {
                    _logger.Warn($"Update failed. Contact with ID {id} not found.");
                    return null;
                }

                _mapper.Map(request, existingContact);
                await _context.SaveChangesAsync();
                _logger.Info($"Contact updated: {id}");

                return _mapper.Map<AddressEntry>(existingContact);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error updating contact with ID: {id}");
                throw;
            }
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            try
            {
                var contactToDelete = _context.AddressBookEntries.FirstOrDefault(e => e.Id == id);
                if (contactToDelete == null)
                {
                    _logger.Warn($"Delete failed. Contact with ID {id} not found.");
                    return false;
                }

                _context.AddressBookEntries.Remove(contactToDelete);
                await _context.SaveChangesAsync();
                _logger.Info($"Contact deleted: {id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error deleting contact with ID: {id}");
                throw;
            }
        }
    }
}