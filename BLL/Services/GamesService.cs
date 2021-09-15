using BLL.Dto;
using BLL.Interfaces;
using DAL.Data;
using RIL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductDto _productDto;
        public GamesService(ApplicationDbContext context)
        {
            _context = context;
            _productDto = new ProductDto(_context);
        }
        public async Task<Dictionary<string, int>> GetTopPlatforms(int quantity)
        {
            List<(int, int)> resultList = await _productDto.GetTopPlatforms();
            resultList.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            Dictionary<string, int> resultDictionary = new();
            for (int i = 0; i < quantity; i++)
            {
                resultDictionary.Add(((Platform)resultList[i].Item1).ToString(), resultList[i].Item2);
            }
            return resultDictionary;
        }

        public async Task<List<Product>> SearchGamesByName(string name)
        {
            return await _productDto.GetProductsByName(name);
        }
    }
}
