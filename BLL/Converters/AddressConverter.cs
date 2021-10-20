using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using RIL.Models;

namespace BLL.Converters
{
    public class AddressConverter : IAddressConverter
    {
        private readonly IMapper _mapper;

        public AddressConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public AddressViewModel ConvertToAddressViewModel(Address address)
        {
            return _mapper.Map<AddressViewModel>(address);
        }

        public Address ConvertToAddress(AddressViewModel address)
        {
            return _mapper.Map<Address>(address);
        }
    }
}
