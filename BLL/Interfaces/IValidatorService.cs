namespace BLL.Interfaces
{
    public interface IValidatorService
    {
        public bool IsValidEmail(string email);
        public bool IsValidPassword(string password);
        public bool IsValidPhoneNumber(string phoneNumber);
    }
}
