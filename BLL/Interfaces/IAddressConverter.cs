using BLL.ViewModels;
using RIL.Models;

namespace BLL.Interfaces
{
    public interface IAddressConverter
    {
        public AddressViewModel ConvertToAddressViewModel(Address address);
        public Address ConvertToAddress(AddressViewModel address);
    }
}
