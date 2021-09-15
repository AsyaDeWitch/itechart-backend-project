using DAL.Data;
using Microsoft.EntityFrameworkCore;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<(int, int)>> GetEachPlatformCount()
        {
            List<(int, int)> counts = new();
            for(int i = 0; i < Enum.GetNames(typeof(Platform)).Length; i++)
            {
                int temp = await _context.Products.CountAsync(p => p.Platform == i);
                counts.Add((i, temp));
            }
            return counts;
        }
    }
}
