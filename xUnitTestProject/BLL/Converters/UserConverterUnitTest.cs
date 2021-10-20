using AutoFixture;
using AutoFixture.Xunit2;
using BLL.Converters;
using BLL.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Converters
{
    public class UserConverterUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void GivenJsonPatchDocumentWithPatchUserPasswordViewModel_WhenApplyTo_ThenPatchUserPasswordViewModelReturned(UserConverter sut, [Frozen] JsonPatchDocument<PatchUserPasswordViewModel> jsonPatch)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            //Act
            var result = sut.ApplyTo(jsonPatch);

            //Assert
            Assert.IsAssignableFrom<PatchUserPasswordViewModel>(result);
        }
    }
}
