using RIL.Models;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAddressRepository
    {
        public Task<Address> GetByIdAsync(int? id);
    }
}
