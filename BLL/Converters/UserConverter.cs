using BLL.Interfaces;
using BLL.ViewModels;
using Microsoft.AspNetCore.JsonPatch;

namespace BLL.Converters
{
    public class UserConverter : IUserConverter
    {
        public PatchUserPasswordViewModel ApplyTo(JsonPatchDocument<PatchUserPasswordViewModel> user)
        {
            var patchedUser = new PatchUserPasswordViewModel();
            user.ApplyTo(patchedUser);
            return patchedUser;
        }
    }
}
