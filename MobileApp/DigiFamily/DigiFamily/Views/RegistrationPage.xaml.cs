using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiFamily.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new RegistrationViewModel();
        }
        private void passwordEye_Clicked(object sender, System.EventArgs e)
        {
            if (PasswordEntry.IsPassword)
            {
                PasswordEntry.IsPassword = false;
                passwordEyeIcon.Source = new FontImageSource
                {
                    FontFamily = "FontAwesome",
                    Glyph = IconFont.EyeOff,
                    Color = CommonHelper.GetResource<Color>("IconColor"),
                    Size = 20
                };
            }
            else
            {
                PasswordEntry.IsPassword = true;
                passwordEyeIcon.Source = new FontImageSource
                {
                    FontFamily = "FontAwesome",
                    Glyph = IconFont.Eye,
                    Color = CommonHelper.GetResource<Color>("IconColor"),
                    Size = 20
                };
            }
        }
        private void confirmPasswordEye_Clicked(object sender, System.EventArgs e)
        {
            ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
        }
    }
}