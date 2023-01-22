using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.Interfaces;
using DigiFamily.Models;
using DigiFamily.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiFamily.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties
        private string username;
        private string password;
        private bool isRememberMe;
        private bool isUsernameValidationSuccess = true;
        private bool isPasswordValidationSuccess = true;
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command ForgotPasswordCommand { get; }
        public bool IsUsernameValidationSuccess
        {
            get => isUsernameValidationSuccess;
            set => SetProperty(ref isUsernameValidationSuccess, value);
        }
        public bool IsPasswordValidationSuccess
        {
            get => isPasswordValidationSuccess;
            set => SetProperty(ref isPasswordValidationSuccess, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public bool IsRememberMe
        {
            get => isRememberMe;
            set => SetProperty(ref isRememberMe, value);
        }

        public IAccountService AccountService => DependencyService.Get<IAccountService>();
        #endregion

        public LoginViewModel()
        {
            //Username = "sasindran.thavaselvam@rencata.com1";
            //Password = "Client@123";
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
            ForgotPasswordCommand = new Command(OnForgotPasswordClicked);
        }

        private void OnForgotPasswordClicked(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                Shell.Current.CurrentPage.ShowPopup(new ForgotPasswordPopupPage(new ForgotPasswordViewModel()));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnRegisterClicked(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                await Shell.Current.GoToAsync($"RegistrationPage");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                string validationMsg = string.Empty;
                Logger.Info("Login clicked", new Dictionary<string, string> { { "Username", Username } });
                await SecureStorage.SetAsync("IsRemember", IsRememberMe.ToString());
                if (string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
                {
                    IsUsernameValidationSuccess = false;
                    IsPasswordValidationSuccess = false;
                    validationMsg = " Username and Password are required.";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Username))
                    {
                        IsUsernameValidationSuccess = false;
                        validationMsg += " Username is required.";
                    }
                    else if (!Username.IsValidEmail())
                    {
                        IsUsernameValidationSuccess = false;
                        validationMsg += " Username is invalid.";
                    }
                    else
                        IsUsernameValidationSuccess = true;
                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        IsPasswordValidationSuccess = false;
                        validationMsg += " Password is required.";
                    }
                    else
                        IsPasswordValidationSuccess = true;
                }
                if (!string.IsNullOrEmpty(validationMsg))
                    await CommonHelper.ShowToastAsync(validationMsg, ToastType.Error);
                else
                {
                    IsUsernameValidationSuccess = true;
                    IsPasswordValidationSuccess = true;
                    if (await AccountService.LogInAsync(Username, Password))
                    {
                        if (IsRememberMe)
                        {
                            await SecureStorage.SetAsync("Username", Username);
                            await SecureStorage.SetAsync("Password", Password);
                        }
                        else
                        {
                            SecureStorage.Remove("Username");
                            SecureStorage.Remove("Password");
                        }
                        MessagingCenter.Send<Application>(Application.Current, "UpdateProfile");
                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    }
                }
            }
            catch (Exception ex)
            {
                await CommonHelper.ShowToastAsync(" Unable to process the request. Please try again.", ToastType.Error);
                Logger.Error(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}


