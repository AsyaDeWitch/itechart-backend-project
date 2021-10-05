using DAL.Data;
using DAL.Repositories;
using RIL.Models;
using System.Threading.Tasks;

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

        public async Task<Address> GetAddressById(int? id)
        {
            if (id != null)
            {
                return await _addressRepository.GetById(id);
            }
            return null;
        }
    }
}
