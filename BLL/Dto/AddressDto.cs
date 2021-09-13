using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Data;
using DAL.Repositories;
using RIL.Models;

namespace BLL.Dto
{
    public class AddressDto
    {
        private readonly ApplicationDbContext _context;
        private readonly AddressRepository _addressRepository;

        public AddressDto (ApplicationDbContext context)
        {
            _context = context;
            _addressRepository = new AddressRepository(_context);
        }

        public Address GetAddressById(int id)
        {
            var address = _addressRepository.GetById(id);
            if (address != null)
            {
                return address;
            }
            return null;
        }
    }
}
