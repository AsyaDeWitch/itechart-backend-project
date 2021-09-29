using DAL.Data;
using Microsoft.EntityFrameworkCore;
using RIL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address> GetById(int? id)
        {
            return await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
