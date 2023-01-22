using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Windows.Input;
using Telerik.XamarinForms.Primitives.CheckBox.Commands;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiFamily.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        #region Properties
        private string firstname;
        private string lastname;
        private string email;
        private DateTime dob = DateTime.Now;
        private GenderType gender = GenderType.Male;
        private DetailPageType pagetype;
        private long userId;
        private bool isFamilyHead = false;
        private string buttonCaption;
        private string phone;
        private bool isPhoneValidationSuccess = true;
        private bool isEmailValidationSuccess = true;
        private bool isFirstnameValidationSuccess = true;
        private bool isDOBValidationSuccess = true;
        private ObservableCollection<DataViewModel> emails;
        private ObservableCollection<DataViewModel> phones;
        private ObservableCollection<CertificateViewModel> certificates;

        public DetailPageType PageType { get => pagetype; set => SetProperty(ref pagetype, value); }
        public long UserId { get => userId; set => SetProperty(ref userId, value); }
        public bool IsFamilyHead { get => isFamilyHead; set => SetProperty(ref isFamilyHead, value); }
        public string ButtonCaption { get => buttonCaption; set => SetProperty(ref buttonCaption, value); }

        public Command SubmitCommand { get; }
        public Command BackPageCommand { get; }
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
        public bool IsDOBValidationSuccess
        {
            get => isDOBValidationSuccess;
            set => SetProperty(ref isDOBValidationSuccess, value);
        }
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        public bool IsPhoneValidationSuccess { get => isPhoneValidationSuccess; set => SetProperty(ref isPhoneValidationSuccess, value); }
        public ObservableCollection<DataViewModel> Emails { get => emails; set => SetProperty(ref emails, value); }
        public ObservableCollection<DataViewModel> Phones { get => phones; set => SetProperty(ref phones, value); }
        public ObservableCollection<CertificateViewModel> Certificates { get => certificates; set => SetProperty(ref certificates, value); }
        #endregion

        public UserDetailsViewModel()
        {
            Certificates = new ObservableCollection<CertificateViewModel>();
            Emails = new ObservableCollection<DataViewModel>();
            Phones = new ObservableCollection<DataViewModel>();
            BackPageCommand = new Command(OnBackPage);
            SubmitCommand = new Command(OnSubmitClicked);
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

        private void OnSubmitClicked(object obj)
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

        private async void OnBackPage()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query == null)
                return;
            if (query.ContainsKey("userid"))
            {
                string userId = HttpUtility.UrlDecode(query["userid"]);
                UserId = !string.IsNullOrEmpty(userId) ? Convert.ToInt64(userId) : 0;
            }

            if (query.ContainsKey("pagetype"))
            {
                string pageType = HttpUtility.UrlDecode(query["pagetype"]);
                if (!string.IsNullOrEmpty(pageType))
                {
                    PageType = pageType.ToEnum<DetailPageType>();
                    if (PageType == DetailPageType.Add)
                    {
                        Title = "Add";
                        ButtonCaption = "Add";
                    }
                    if (PageType == DetailPageType.Update)
                    {
                        Title = "Update";
                        ButtonCaption = "Update";
                        Certificates.Add(new CertificateViewModel()
                        {
                            CertificateType = CertificateType.Aadhaar,
                            IsAddVisible = false,
                            IsViewVisible = true
                        }); ;

                    }
                }
            }

            if (query.ContainsKey("query"))
            {
                string isFamilyHead = HttpUtility.UrlDecode(query["query"]);
                if (!string.IsNullOrEmpty(isFamilyHead))
                    IsFamilyHead = Convert.ToBoolean(isFamilyHead);
            }
        }
    }

    public class DataViewModel : BaseViewModel
    {
        private int id;
        private string data;
        private bool isValid = true;

        public string Data { get => data; set => SetProperty(ref data, value); }
        public bool IsValid { get => isValid; set => SetProperty(ref isValid, value); }
        public int Id { get => id; set => SetProperty(ref id, value); }
    }

    public class CertificateViewModel : BaseViewModel
    {
        private int id;
        private CertificateType certificateType;
        private ObservableCollection<CertificateType> certificateTypes;
        private string certificateName;
        private FileResult certificate;
        private bool isNameVisible = false;
        private bool isValid = true;
        private bool isAddVisible = false;
        private bool isViewVisible = true;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public CertificateType CertificateType { get => certificateType; set => SetProperty(ref certificateType, value); }
        public string CertificateName { get => certificateName; set => SetProperty(ref certificateName, value); }
        public FileResult Certificate { get => certificate; set => SetProperty(ref certificate, value); }
        public bool IsNameVisible { get => isNameVisible; set => SetProperty(ref isNameVisible, value); }
        public bool IsValid { get => isValid; set => SetProperty(ref isValid, value); }
        public ObservableCollection<CertificateType> CertificateTypes { get => certificateTypes; set => SetProperty(ref certificateTypes, value); }
        public bool IsAddVisible { get => isAddVisible; set => SetProperty(ref isAddVisible, value); }
        public bool IsViewVisible { get => isViewVisible; set => SetProperty(ref isViewVisible, value); }
        public ICommand Accept { get; set; }
        public ICommand Cancel { get; set; }

        public CertificateViewModel()
        {
            CertificateTypes = new ObservableCollection<CertificateType>()
            {
                CertificateType.Aadhaar,
                CertificateType.Birth,
                CertificateType.DL,
                CertificateType.PAN,
                CertificateType.Passport,
                CertificateType.Others
            };
            this.Accept = new Command(this.OnAccept);
            this.Cancel = new Command(this.OnCancel);
        }
        private void OnAccept(object param)
        {
            if (param != null && param is CertificateType certificateType)
            {
                if (certificateType == CertificateType.Others)
                    this.IsNameVisible = true;
                else
                    this.IsNameVisible = false;
            }
        }

        private void OnCancel(object param)
        {
        }
    }
}
