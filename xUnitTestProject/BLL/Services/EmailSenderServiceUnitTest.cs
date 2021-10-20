using BLL.Services;
using System.Threading.Tasks;
using Xunit;
using xUnitTestProject.AutoFakeItEasyDataAttributes;

namespace xUnitTestProject.BLL.Services
{
    public class EmailSenderServiceUnitTest
    {
        [Theory]
        [InlineAutoFakeItEasyData("itechartlabtester@gmail.com", "<p><a href = google.com\"\">Follow the link to confirm your email</a></p>")]
        [InlineAutoFakeItEasyData("kek@gmail.com", "<p><a href = google.com\"\">Follow the link to confirm your email</a></p>")]
        public void GivenEmailAndMessage_WhenSendEmailByNetMailAsync_ThenNothingReturned(string email, string htmlMessage, EmailSenderService sut)
        {
            // Arrange

            //Act
            var task = sut.SendEmailByNetMailAsync(email, htmlMessage);
            Task.WaitAll(task);

            //Assert
            var status = task.Status;
            Assert.Equal(TaskStatus.RanToCompletion, status);
        }

        [Theory]
        [InlineAutoFakeItEasyData("itechartlabtester@gmail.com", "<p><a href = google.com\"\">Follow the link to confirm your email</a></p>")]
        [InlineAutoFakeItEasyData("kek@gmail.com", "<p><a href = google.com\"\">Follow the link to confirm your email</a></p>")]
        public void GivenEmailAndMessage_WhenSendEmailByMailKitAsync_ThenNothingReturned(string email, string htmlMessage, EmailSenderService sut)
        {
            // Arrange

            //Act
            var task = sut.SendEmailByMailKitAsync(email, htmlMessage);
            Task.WaitAll(task);

            //Assert
            var status = task.Status;
            Assert.Equal(TaskStatus.RanToCompletion, status);
        }
    }
}
