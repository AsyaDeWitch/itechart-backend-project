using DAL.Data;
using DAL.Interfaces;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Address GetById(int id)
        {
            using(_context)
            {
                Address address = _context.Addresses
                    .Find(id);
                if (address != null)
                {
                    return address;
                }
            }
            return null;
        }
    }
}
