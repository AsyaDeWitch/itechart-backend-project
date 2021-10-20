using AutoFixture;
using AutoFixture.Xunit2;
using BLL.Services;
using BLL.ViewModels;
using DAL.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Threading.Tasks;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Services
{
    public class AdministrationServiceUnitTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenEmailAndRoleName_WhenAssignRoleToUserAsync_ThenIdentityResultReturned(string email, string roleName, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns(user);
            A.CallTo(() => service.ExtendedUsers.RoleExistsAsync(roleName))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.AddToRoleAsync(user, roleName))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.AssignRoleToUserAsync(email, roleName);

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [InlineAutoFakeItEasyData(true)]
        [InlineAutoFakeItEasyData(false)]
        public async Task GivenEmailAndRoleName_WhenAssignRoleToUserAsync_ThenFailedIdentityResultReturned(bool isRoleExists, string email, string roleName, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns(user);
            A.CallTo(() => service.ExtendedUsers.RoleExistsAsync(roleName))
                .Returns(isRoleExists);
            A.CallTo(() => service.ExtendedUsers.AddToRoleAsync(user, roleName))
                .Returns(IdentityResult.Failed());

            //Act
            var result = await sut.AssignRoleToUserAsync(email, roleName);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNonExistentEmail_WhenAssignRoleToUserAsync_ThenFailedIdentityResultReturned(string email, string roleName, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns<ExtendedUser>(null);

            //Act
            var result = await sut.AssignRoleToUserAsync(email, roleName);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenRoleName_WhenCreateRoleAsync_ThenIdentityResultReturned(string roleName, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            A.CallTo(() => service.ExtendedUsers.RoleExistsAsync(roleName))
                .Returns(false);
            A.CallTo(() => service.ExtendedUsers.CreateRoleAsync(roleName))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.CreateRoleAsync(roleName);

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [InlineAutoFakeItEasyData(true)]
        [InlineAutoFakeItEasyData(false)]
        public async Task GivenRoleName_WhenCreateRoleAsync_ThenFailedIdentityResultReturned(bool isRoleExists, string roleName, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            A.CallTo(() => service.ExtendedUsers.RoleExistsAsync(roleName))
                .Returns(isRoleExists);
            A.CallTo(() => service.ExtendedUsers.CreateRoleAsync(roleName))
                .Returns(IdentityResult.Failed(null));

            //Act
            var result = await sut.CreateRoleAsync(roleName);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenRoleName_WhenDeleteRoleAsync_ThenIdentityResultReturned(string roleName, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var role = fixture.Create<IdentityRole<int>>();

            A.CallTo(() => service.ExtendedUsers.RoleExistsAsync(roleName))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.FindRoleByNameAsync(roleName))
                .Returns(role);
            A.CallTo(() => service.ExtendedUsers.DeleteRoleAsync(role))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.DeleteRoleAsync(roleName);

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [InlineAutoFakeItEasyData(true)]
        [InlineAutoFakeItEasyData(false)]
        public async Task GivenRoleName_WhenDeleteRoleAsync_ThenFailedIdentityResultReturned(bool isRoleExists, string roleName, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var role = fixture.Create<IdentityRole<int>>();

            A.CallTo(() => service.ExtendedUsers.RoleExistsAsync(roleName))
                .Returns(isRoleExists);
            A.CallTo(() => service.ExtendedUsers.FindRoleByNameAsync(roleName))
                .Returns(role);
            A.CallTo(() => service.ExtendedUsers.DeleteRoleAsync(role))
                .Returns(IdentityResult.Failed(null));

            //Act
            var result = await sut.DeleteRoleAsync(roleName);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenEmail_WhenDeleteUserByEmailAsync_ThenIdentityResultReturned(string email, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns(user);
            A.CallTo(() => service.ExtendedUsers.DeleteAsync(user))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.DeleteUserByEmailAsync(email);

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNonExistentEmail_WhenDeleteRoleAsync_ThenFailedIdentityResultReturned(string email, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns<ExtendedUser>(null);

            //Act
            var result = await sut.DeleteUserByEmailAsync(email);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenEmail_WhenDeleteRoleAsync_ThenFailedIdentityResultReturned(string email, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByEmailAsync(email))
                .Returns(user);
            A.CallTo(() => service.ExtendedUsers.DeleteAsync(user))
                .Returns(IdentityResult.Failed(null));

            //Act
            var result = await sut.DeleteUserByEmailAsync(email);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserId_WhenDeleteUserByIdAsync_ThenIdentityResultReturned(string id, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(id))
                .Returns(user);
            A.CallTo(() => service.ExtendedUsers.DeleteAsync(user))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.DeleteUserByIdAsync(id);

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNonExistentUserId_WhenDeleteUserByIdAsync_ThenFailedIdentityResultReturned(string id, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(id))
                .Returns<ExtendedUser>(null);

            //Act
            var result = await sut.DeleteUserByIdAsync(id);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenUserId_WhenDeleteUserByIdAsync_ThenFailedIdentityResultReturned(string id, [Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();

            A.CallTo(() => service.ExtendedUsers.FindByIdAsync(id))
                .Returns(user);
            A.CallTo(() => service.ExtendedUsers.DeleteAsync(user))
                .Returns(IdentityResult.Failed(null));

            //Act
            var result = await sut.DeleteUserByIdAsync(id);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenPatchUserRole_WhenUpdateRoleAsync_ThenIdentityResultReturned([Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var role = fixture.Create<IdentityRole<int>>();
            var updatedRole = fixture.Create<PatchUserRoleViewModel>();

            A.CallTo(() => service.ExtendedUsers.RoleExistsAsync(updatedRole.CurrentRole))
                .Returns(true);
            A.CallTo(() => service.ExtendedUsers.FindRoleByNameAsync(updatedRole.CurrentRole))
                .Returns(role);
            role.Name = updatedRole.NewRole;
            A.CallTo(() => service.ExtendedUsers.UpdateRoleAsync(role))
                .Returns(IdentityResult.Success);

            //Act
            var result = await sut.UpdateRoleAsync(updatedRole);

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [AutoFakeItEasyData]
        public async Task GivenNonExistentCurrentRole_WhenUpdateRoleAsync_ThenFailedIdentityResultReturned([Frozen] IUnitOfWork service, AdministrationService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var updatedRole = fixture.Create<PatchUserRoleViewModel>();

            A.CallTo(() => service.ExtendedUsers.RoleExistsAsync(updatedRole.CurrentRole))
                .Returns(false);

            //Act
            var result = await sut.UpdateRoleAsync(updatedRole);

            //Assert
            Assert.NotEqual(IdentityResult.Success, result);
        }
    }
}
