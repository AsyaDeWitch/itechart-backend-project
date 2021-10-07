using BLL.Services;
using Xunit;

namespace xUnitTestProject.BLL.Services
{
    public class ValidatorServiceUnitTest
    {
        [Theory]
        [InlineData("test@gmail.com", true)]
        [InlineData("test@itechart-group.com", true)]
        [InlineData("test12345@mail.ru", true)]
        [InlineData("test@", false)]
        [InlineData("@gmail.com", false)]
        public void GivenEmailString_WhenIsValidEmail_ThenBoolResultReturned(string email, bool expected)
        {
            // Arrange
            var sut = new ValidatorService();

            //Act
            var result = sut.IsValidEmail(email);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("12345678", false)]
        [InlineData("qwertyui", false)]
        [InlineData("QWERTYUI", false)]
        [InlineData("QWERtyui", false)]
        [InlineData("QWERtyu1", false)]
        [InlineData("!QWERty1", true)]
        public void GivenPasswordString_WhenIsValidPassword_ThenBoolResultReturned(string password, bool expected)
        {
            //Arrange
            var sut = new ValidatorService();

            //Act
            var result = sut.IsValidPassword(password);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("123-456-78", true)]
        [InlineData("+375290000000", true)]
        [InlineData("80-29-000-00-00", true)]
        [InlineData("12345678", true)]
        [InlineData("qwerty", false)]
        public void GivenPhoneNumberString_WhenIsValidPhoneNumber_ThenBoolResultReturned(string phoneNumber, bool expected)
        {
            //Arrange
            var sut = new ValidatorService();

            //Act
            var result = sut.IsValidPhoneNumber(phoneNumber);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
