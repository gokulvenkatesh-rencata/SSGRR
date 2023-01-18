using DigiFamily.Interfaces;
using DigiFamily.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiFamily.Services
{
    public class AccountService : BaseService, IAccountService
    {
        public Task<string> ForgotPasswordAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> LogInAsync(string username, string password)
        {
            bool rValue = false;
            Dictionary<string, string> formData = new Dictionary<string, string>
            {
                {"client_id", Config.ClienID},
                {"client_secret",Config.ClientSecret},
                {"grant_type", "password"},
                {"username", username},
                {"password", password},
                {"appVersion", "7.1.1"}
            };
            using (HttpContent content = new FormUrlEncodedContent(formData))
            {
                APIToken token = await HttpService.AnonymousPostAsync<APIToken>("Token", content);
                if (token != null && !string.IsNullOrEmpty(token.Access_Token))
                {
                    rValue = true;
                    await SecureStorage.SetAsync("oauth_token", token.Access_Token);
                    await SecureStorage.SetAsync("oauth_username", token.UserName);
                    await SecureStorage.SetAsync("token_expires", token.Expires);
                }
            };
            return rValue;
        }
    }
}
