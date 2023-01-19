using DigiFamily.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using DigiFamily.Helpers;
using DigiFamily.Models;
using System;
using DigiFamily.Helpers.Utils;

namespace DigiFamily.Views
{
    public partial class ForgotPasswordPopupPage : Popup<ForgotPasswordViewModel>
    {
        private ForgotPasswordViewModel ViewModel;
        public ForgotPasswordPopupPage(ForgotPasswordViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = ViewModel = viewModel;
            App.ForgotPasswordPopup = this;
        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private async void ForgotPasswordButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ViewModel.IsBusy)
                    return;
                ViewModel.IsBusy = true;
                string result = "Password reset requested successfully. Please check your email.";
                ClosePopup();
                await CommonHelper.ShowToastAsync(result, ToastType.Success);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                ViewModel.IsBusy = false;
            }
        }

        protected void ClosePopup()
        {
            App.AlertPopup = null;
            Dismiss(null);
        }
    }
}