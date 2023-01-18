using Newtonsoft.Json;
using DigiFamily.Helpers.Utils;
using DigiFamily.Models;
using DigiFamily.Services;
using DigiFamily.Views.Common;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiFamily.Helpers
{
    public static class CommonHelper
    {
        public static async Task<bool> GetIsConnecttedAsync()
        {
            bool rValue = false;
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.GoToAsync("//LoginPage");
                ShowAlert("No Internet connection. Please make sure that Wi-Fi or mobile data is turned on, then try again.");
            }
            else
                rValue = true;
            return rValue;
        }
        public static async Task ShowToastAsync(string msg, ToastType toastType)
        {
            ToastContent toastContent = new ToastContent()
            {
                Message = msg,
                ToastType = toastType
            };
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ToastPage(toastContent));
        }
        public static void ShowAlert(string msg, string title = "Alert", string okBtn = "OK", AlertType alertType = AlertType.Notify)
        {
            if (title != "Logout")
            {
                AlertContent alertContent = new AlertContent()
                {
                    Title = title,
                    Message = msg,
                    OkBtn = okBtn,
                    AlertType = alertType
                };
                Shell.Current.CurrentPage.ShowPopup(new AlertPopupPage(alertContent));
            }
            else
                Shell.Current.CurrentPage.DisplayAlert("Alert", msg, okBtn);
        }
        public static async Task<bool> ShowConfirmAsync(string ConfirmMsg, string title = "Confirm", string confirmBtn = "Yes", string cancelBtn = "No")
        {
            bool retVal = false;
            AlertContent alertContent = new AlertContent()
            {
                Title = title,
                Message = ConfirmMsg,
                NoBtn = cancelBtn,
                YesBtn = confirmBtn,
                AlertType = AlertType.Confirm
            };
            AlertContent result = await Shell.Current.CurrentPage.ShowPopupAsync(new AlertPopupPage(alertContent));
            if (result != null)
                retVal = result.IsConfirmed;
            return retVal;
            //bool isConfirmed = await Current.MainPage.DisplayAlert(title, ConfirmMsg, confirmBtn, cancelBtn);
            //return isConfirmed;
        }
        public static async Task<string> ShowActionSheetAsync(string[] Actions, string title = "ActionSheet", string cancelBtn = "Cancel")
        {
            return await Shell.Current.CurrentPage.DisplayActionSheet(title, cancelBtn, null, Actions);
        }

        public static T ConvertToObject<T>(string strResult)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(strResult);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return default(T);
            }
        }

        public static T DeserializeJsonFromStream<T>(Stream stream)
        {
            try
            {
                if (stream == null || stream.CanRead == false)
                    return default(T);

                using (StreamReader sr = new StreamReader(stream))
                using (JsonTextReader jtr = new JsonTextReader(sr))
                {
                    T searchResult = new JsonSerializer().Deserialize<T>(jtr);
                    return searchResult;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return default(T);
            }
        }

        public static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = string.Empty;
            try
            {
                if (stream != null)
                    using (StreamReader sr = new StreamReader(stream))
                        content = await sr.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return content;
        }

        public static async Task<string> GetAuthTokenAsync()
        {
            string oauthToken = string.Empty;
            try
            {
                oauthToken = await SecureStorage.GetAsync("oauth_token");
                if (string.IsNullOrEmpty(oauthToken) && Shell.Current != null)
                    await Shell.Current.GoToAsync("//LoginPage");
                else
                {
                    string expires = await SecureStorage.GetAsync("token_expires");
                    if (!string.IsNullOrEmpty(expires))
                    {
                        DateTime expiresDate = Convert.ToDateTime(expires).AddMinutes(-30);
                        if (!(DateTime.Now < expiresDate))
                        {
                            oauthToken = string.Empty;
                            await LogoutAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return oauthToken;
        }

        public static async Task LogoutAsync(bool isConfirm = false)
        {
            try
            {
                if (isConfirm)
                {
                    if (await ShowConfirmAsync("Are you sure want to logout?", title: "Logout"))
                        await logoutAsync();
                }
                else
                    await logoutAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        static async Task logoutAsync()
        {
            try
            {
                SecureStorage.Remove("oauth_token");
                SecureStorage.Remove("token_expires");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public static async Task SetLoadingAsync(bool isLoading)
        {
            if (isLoading)
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new OverlayLoadingPopupPage());
            else
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAllAsync();
        }

        public static void SetTheme(OSAppTheme appTheme)
        {
            switch (appTheme)
            {
                case OSAppTheme.Light:
                    string darkTheme = Preferences.Get("OSAppTheme", Enum.GetName(typeof(OSAppTheme), OSAppTheme.Dark));
                    Application.Current.UserAppTheme = (OSAppTheme)Enum.Parse(typeof(OSAppTheme), darkTheme);
                    Application.Current.Resources["IconColor"] = Color.Gray;
                    break;
                case OSAppTheme.Unspecified:
                case OSAppTheme.Dark:
                default:
                    string lightTheme = Preferences.Get("OSAppTheme", Enum.GetName(typeof(OSAppTheme), OSAppTheme.Dark));
                    Application.Current.UserAppTheme = (OSAppTheme)Enum.Parse(typeof(OSAppTheme), lightTheme);
                    Application.Current.Resources["IconColor"] = Color.LightGray;
                    break;
            }
        }
        public static T GetResource<T>(string resourceName)
        {
            try
            {
                bool success = Application.Current.Resources.TryGetValue(resourceName, out object outValue);
                return success && outValue is T t ? t : default;
            }
            catch
            {
                return default;
            }
        }
    }
}
