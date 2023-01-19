namespace DigiFamily.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        #region Properties
        private string username;
        private bool isUsernameValidationSuccess = true;

        public string Username { get => username; set => SetProperty(ref username, value); }
        public bool IsUsernameValidationSuccess
        {
            get => isUsernameValidationSuccess;
            set => SetProperty(ref isUsernameValidationSuccess, value);
        }
        public ForgotPasswordViewModel()
        {
            Title = "Forgot Password";
        }
        #endregion
    }
}
