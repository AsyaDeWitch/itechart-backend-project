using System;
using BLL.Interfaces;
using BLL.ViewModels;
using Microsoft.Extensions.Caching.Memory;

namespace BLL.Cachers
{
    public class MemoryCacher : IMemoryCacher
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacher(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Remove(string userId)
        {
            _memoryCache.Remove(userId);
        }

        public bool TryGetValue(string userId, out ReturnUserProfileViewModel user)
        {
           return _memoryCache.TryGetValue(userId, out user);
        }

        public ReturnUserProfileViewModel Set(string userId, ReturnUserProfileViewModel user)
        {
            return _memoryCache.Set(userId, user, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
                SlidingExpiration = TimeSpan.FromHours(6),
            });
        }
    }
}
