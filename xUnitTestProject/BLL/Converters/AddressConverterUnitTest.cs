using AutoFixture;
using BLL.Converters;
using BLL.ViewModels;
using RIL.Models;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Converters
{
    public class AddressConverterUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenAddressViewModel_WhenConvertToAddressViewModel_ThenAddressReturned(AddressConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var address = fixture.Create<AddressViewModel>();

            //Act
            var result = sut.ConvertToAddress(address);

            //Assert
            Assert.IsAssignableFrom<Address>(result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void GivenAddress_WhenConvertToAddress_ThenAddressViewModelReturned(AddressConverter sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var address = fixture.Create<Address>();

            //Act
            var result = sut.ConvertToAddressViewModel(address);

            //Assert
            Assert.IsAssignableFrom<AddressViewModel>(result);
        }
    }
}

