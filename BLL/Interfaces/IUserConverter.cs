using BLL.ViewModels;
using Microsoft.AspNetCore.JsonPatch;

namespace BLL.Interfaces
{
    public interface IUserConverter
    {
        public PatchUserPasswordViewModel ApplyTo(JsonPatchDocument<PatchUserPasswordViewModel> user);
    }
}
