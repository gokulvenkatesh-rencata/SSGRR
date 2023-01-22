using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.Models;
using DigiFamily.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Telerik.XamarinForms.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiFamily.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailsPage : ContentPage
    {
        private UserDetailsViewModel ViewModel;
        int listHeight = 60;
        public UserDetailsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new UserDetailsViewModel();
        }
        private async void CertificateDelete_TappedAsync(object sender, EventArgs e)
        {
            if (ViewModel.IsBusy)
                return;
            try
            {
                if (sender is RadButton deleteBtn)
                {
                    CertificateViewModel certificate = (CertificateViewModel)deleteBtn.BindingContext;
                    if (certificate != null)
                    {
                        bool isDelete = true;
                        if (certificate.Id > 0 || !string.IsNullOrEmpty(certificate.CertificateName))
                            isDelete = await CommonHelper.ShowConfirmAsync("Are you sure want to remove this certificate?");
                        if (isDelete)
                            ViewModel.Certificates.Remove(certificate);
                    }
                    CertificateList.EndItemSwipe(true);
                    CertificateList.HeightRequest = ViewModel.Certificates.Count * listHeight;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private async void EmailDelete_TappedAsync(object sender, EventArgs e)
        {
            if (ViewModel.IsBusy)
                return;
            try
            {
                if (sender is RadButton deleteBtn)
                {
                    DataViewModel email = (DataViewModel)deleteBtn.BindingContext;
                    if (email != null)
                    {
                        bool isDelete = true;
                        if (email.Id > 0 || !string.IsNullOrEmpty(email.Data))
                            isDelete = await CommonHelper.ShowConfirmAsync("Are you sure want to remove this email?");
                        if (isDelete)
                            ViewModel.Emails.Remove(email);
                    }
                    EmailList.EndItemSwipe(true);
                    EmailList.HeightRequest = ViewModel.Emails.Count * listHeight;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private async void PhoneDelete_TappedAsync(object sender, EventArgs e)
        {
            if (ViewModel.IsBusy)
                return;
            try
            {
                if (sender is RadButton deleteBtn)
                {
                    DataViewModel phone = (DataViewModel)deleteBtn.BindingContext;
                    if (phone != null)
                    {
                        bool isDelete = true;
                        if (phone.Id > 0 || !string.IsNullOrEmpty(phone.Data))
                            isDelete = await CommonHelper.ShowConfirmAsync("Are you sure want to remove this phone?");
                        if (isDelete)
                            ViewModel.Phones.Remove(phone);
                    }
                    PhoneList.EndItemSwipe(true);
                    PhoneList.HeightRequest = ViewModel.Emails.Count * listHeight;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private void AddPhoneClicked(object obj, EventArgs e)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                ViewModel.Phones.Insert(0, new DataViewModel());
                PhoneList.HeightRequest = ViewModel.Emails.Count * listHeight;
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

        private void AddEmailClicked(object obj, EventArgs e)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                ViewModel.Emails.Insert(0, new DataViewModel());
                EmailList.HeightRequest = ViewModel.Emails.Count * listHeight;
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
        private void AddCertificateMainClicked(object obj, EventArgs e)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                ViewModel.Certificates.Insert(0, new CertificateViewModel()
                {
                    IsAddVisible = true,
                    IsViewVisible = false
                });
                CertificateList.HeightRequest = ViewModel.Certificates.Count * listHeight;
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
        private async void AddCertificateClicked(object obj, EventArgs e)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                if (obj is Button addBtn)
                {
                    CertificateViewModel certificateModel = (CertificateViewModel)addBtn.BindingContext;
                    if (certificateModel != null)
                    {
                        string selected = await CommonHelper.ShowActionSheetAsync(new string[] { FileSource.Camera.ToString(), FileSource.Gallery.ToString() },
                                "Add certificate from");
                        if (!Equals(selected, "Cancel"))
                        {
                            FileSource fileSource = selected.ToEnum<FileSource>();
                            switch (fileSource)
                            {
                                case FileSource.Camera:
                                    certificateModel.Certificate = await MediaPicker.CapturePhotoAsync();
                                    break;
                                case FileSource.Gallery:
                                    certificateModel.Certificate = await MediaPicker.PickPhotoAsync();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
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
        private void ViewCertificateClicked(object obj, EventArgs e)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;

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

        private async void ProfilePic_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                string selected = await CommonHelper.ShowActionSheetAsync(new string[] { FileSource.Camera.ToString(), FileSource.Gallery.ToString() },
                                "Add profile picture from");
                if (!Equals(selected, "Cancel"))
                {
                    FileResult fileResult;
                    FileSource fileSource = selected.ToEnum<FileSource>();
                    switch (fileSource)
                    {
                        case FileSource.Camera:
                            fileResult = await MediaPicker.CapturePhotoAsync();
                            ProfileImg.Source = await CommonHelper.GetImageSourceAsync(fileResult);
                            break;
                        case FileSource.Gallery:
                            fileResult = await MediaPicker.PickPhotoAsync();
                            ProfileImg.Source = await CommonHelper.GetImageSourceAsync(fileResult);
                            break;
                        default:
                            break;
                    }
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
    }
}