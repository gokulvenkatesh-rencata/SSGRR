using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.XamarinForms.Primitives.CheckBox.Commands;
using Xamarin.Forms;

namespace DigiFamily.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        #region Properties
        private string firstname;
        private string lastname;
        private string email;
        private DateTime dob = DateTime.Now;
        private GenderType gender = GenderType.Male;
        private bool isFamilyHead = true;
        private string password;
        private string confirmPassword;
        private string phone;
        private bool isPhoneValidationSuccess = true;
        private bool isEmailValidationSuccess = true;
        private bool isFirstnameValidationSuccess = true;
        //private bool isLastnameValidationSuccess = true;
        private bool isDOBValidationSuccess = true;
        private bool isPasswordValidationSuccess = true;
        private bool isConfirmPasswordValidationSuccess = true;

        public Command BackPageCommand { get; }
        public Command RegisterCommand { get; }
        public Command IsFemaleCommand { get; }
        public Command IsMaleCommand { get; }

        public bool IsFemale => Gender == GenderType.Female ? true : false;
        public bool IsMale => Gender == GenderType.Male ? true : false;
        public string Firstname
        {
            get => firstname;
            set => SetProperty(ref firstname, value);
        }
        public string Lastname
        {
            get => lastname;
            set => SetProperty(ref lastname, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }
        public DateTime DOB
        {
            get => dob;
            set => SetProperty(ref dob, value);
        }
        public GenderType Gender
        {
            get => gender;
            set
            {
                SetProperty(ref gender, value);
                OnPropertyChanged(nameof(IsFemale));
                OnPropertyChanged(nameof(IsMale));
            }
        }
        public bool IsEmailValidationSuccess
        {
            get => isEmailValidationSuccess;
            set => SetProperty(ref isEmailValidationSuccess, value);
        }
        public bool IsFirstnameValidationSuccess
        {
            get => isFirstnameValidationSuccess;
            set => SetProperty(ref isFirstnameValidationSuccess, value);
        }
        //public bool IsLastnameValidationSuccess
        //{
        //    get => isLastnameValidationSuccess;
        //    set => SetProperty(ref isLastnameValidationSuccess, value);
        //}
        public bool IsDOBValidationSuccess
        {
            get => isDOBValidationSuccess;
            set => SetProperty(ref isDOBValidationSuccess, value);
        }
        public bool IsPasswordValidationSuccess
        {
            get => isPasswordValidationSuccess;
            set => SetProperty(ref isPasswordValidationSuccess, value);
        }
        public bool IsConfirmPasswordValidationSuccess
        {
            get => isConfirmPasswordValidationSuccess;
            set => SetProperty(ref isConfirmPasswordValidationSuccess, value);
        }
        public bool IsFamilyHead { get => isFamilyHead; set => SetProperty(ref isFamilyHead, value); }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public bool IsPhoneValidationSuccess { get => isPhoneValidationSuccess; set => SetProperty(ref isPhoneValidationSuccess, value); }
        #endregion
        public RegistrationViewModel()
        {
            Title = "Registration";
            BackPageCommand = new Command(OnBackPage);
            RegisterCommand = new Command(OnRegisterAsync);
            IsFemaleCommand = new Command(OnFemaleChecked);
            IsMaleCommand = new Command(OnMaleChecked);
        }

        private void OnFemaleChecked(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                if (obj is CheckBoxIsCheckChangedCommandContext context && context.NewState == true)
                {
                    Gender = GenderType.Female;
                }
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

        private void OnMaleChecked(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                if (obj is CheckBoxIsCheckChangedCommandContext context && context.NewState == true)
                {
                    Gender = GenderType.Male;
                }
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

        private async void OnRegisterAsync()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                string validationMsg = "\n";
                Logger.Info("Registeration clicked", new Dictionary<string, string> { { "Email", Email } });
                if (string.IsNullOrWhiteSpace(email))
                {
                    IsEmailValidationSuccess = false;
                    validationMsg += "Username/Email is required.\n";
                }
                else if (!Email.IsValidEmail())
                {
                    IsEmailValidationSuccess = false;
                    validationMsg += "Username/Email is invalid.\n";
                }
                else
                    IsEmailValidationSuccess = true;
                if (string.IsNullOrEmpty(Firstname))
                {
                    IsFirstnameValidationSuccess = false;
                    validationMsg += "Firstname is required.\n";
                }
                else
                    IsFirstnameValidationSuccess = true;
                if (string.IsNullOrWhiteSpace(Phone))
                {
                    IsPhoneValidationSuccess = false;
                    validationMsg += "Phone # is required.\n";
                }
                else if (!Phone.IsValidPhoneNumber())
                {
                    IsPhoneValidationSuccess = false;
                    validationMsg += "Phone # is invalid.\n";
                }
                else
                    IsPhoneValidationSuccess = true;
                if (string.IsNullOrWhiteSpace(Password))
                {
                    IsPasswordValidationSuccess = false;
                    validationMsg += "Password is required.\n";
                }
                else if (!Password.IsValidPasswordStrength())
                {
                    IsPasswordValidationSuccess = false;
                    validationMsg += "Password strength is weak.\n";
                }
                else
                    IsPasswordValidationSuccess = true;

                if (string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    IsConfirmPasswordValidationSuccess = false;
                    validationMsg += "Confirm Password is required.\n";
                }
                else if (Password != ConfirmPassword)
                {
                    IsConfirmPasswordValidationSuccess = false;
                    validationMsg += "Confirm Password is not match with password.\n";
                }
                else
                    IsConfirmPasswordValidationSuccess = true;

                if (!string.IsNullOrEmpty(validationMsg))
                    await CommonHelper.ShowToastAsync(validationMsg, ToastType.Error);
                else
                {
                    //if (await AccountService.LogInAsync(Username, Password))
                    //{
                    //    if (IsRememberMe)
                    //    {
                    //        await SecureStorage.SetAsync("Username", Username);
                    //        await SecureStorage.SetAsync("Password", Password);
                    //    }
                    //    else
                    //    {
                    //        SecureStorage.Remove("Username");
                    //        SecureStorage.Remove("Password");
                    //    }
                    //    MessagingCenter.Send<Application>(Application.Current, "UpdateProfile");
                    //    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    //}
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

        private async void OnBackPage()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
