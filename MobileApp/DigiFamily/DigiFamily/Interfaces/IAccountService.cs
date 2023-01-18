using System.Threading.Tasks;

namespace DigiFamily.Interfaces
{
    public interface IAccountService
    {
        Task<bool> LogInAsync(string username, string password);
        Task<string> ForgotPasswordAsync(string email);
        //Task<UserProfile> GetProfileData();
    }
}
