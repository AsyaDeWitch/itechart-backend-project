using RIL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGamesService
    {
        public Task<Dictionary<string, int>> GetTopPlatforms(int quantity);
        public Task<List<Product>> SearchGamesByName(string name);
    }
}
