using BLL.ViewModels;

namespace BLL.Interfaces
{
    public interface IMemoryCacher
    {
        public void Remove(string userId);
        public bool TryGetValue(string userId, out ReturnUserProfileViewModel user);
        public ReturnUserProfileViewModel Set(string userId, ReturnUserProfileViewModel user);
    }
}
