using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using RIL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address> GetByIdAsync(int? id)
        {
            if(id != null)
            {
                return await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            }
            return null;
        }
    }
}
