using AutoFixture;
using BLL.Services;
using RIL.Models;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Services
{
    public class JwtServiceUnitTest
    {
        [Theory]
        [InlineAutoFakeItEasyData("http://localhost:5000/", "http://localhost:5001/", "DzenisevichSecretKey", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI2IiwiZW1haWwiOiJpdGVjaGFydGxhYnRlc3RlckBnbWFpbC5jb20iLCJleHAiOjE2MzQyMzY0MjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvIn0.rxFdaZFmxG1fOOVYBJXN7erZZO6RBgoTxPle_1K0Vf8", true)]
        [InlineAutoFakeItEasyData("issuer", "audience", "key", "token", false)]
        public void GivenParameters_WhenIsTokenValid_ThenBoolResultReturned(string jwtIssuer, string jwtAudience, string jwtKey, string jwtToken, bool expectedResult,JwtService sut)
        {
            // Arrange

            //Act
            var result = sut.IsTokenValid(jwtIssuer, jwtAudience, jwtKey, jwtToken);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineAutoFakeItEasyData( "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI2IiwiZW1haWwiOiJpdGVjaGFydGxhYnRlc3RlckBnbWFpbC5jb20iLCJleHAiOjE2MzQyMzY0MjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvIn0.rxFdaZFmxG1fOOVYBJXN7erZZO6RBgoTxPle_1K0Vf8", true)]
        [InlineAutoFakeItEasyData("token", false)]
        public void GivenToken_WhenExtractUserIdFromToken_ThenUserIdReturned(string jwtToken, bool expectedResult, JwtService sut)
        {
            // Arrange

            //Act
            var result = sut.ExtractUserIdFromToken(jwtToken);

            //Assert
            Assert.Equal(expectedResult, int.TryParse(result, out var number));
        }

        [Theory]
        [InlineAutoFakeItEasyData("http://localhost:5000/", "http://localhost:5001/", "DzenisevichSecretKey")]
        [InlineAutoFakeItEasyData("issuer", "audience")]
        public void GivenParameters_WhenBuildToken_ThenTokenReturned(string jwtIssuer, string jwtAudience, string jwtKey, JwtService sut)
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var user = fixture.Create<ExtendedUser>();

            //Act
            var result = sut.BuildToken(user, jwtIssuer, jwtAudience, jwtKey);

            //Assert
            Assert.NotNull(result);
        }
    }
}
