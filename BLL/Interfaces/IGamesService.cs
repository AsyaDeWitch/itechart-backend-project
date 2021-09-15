using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGamesService
    {
        public Task<Dictionary<string, int>> GetTopPlatforms(int quantity);

    }
}
