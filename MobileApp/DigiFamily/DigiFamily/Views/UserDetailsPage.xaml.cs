using DigiFamily.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiFamily.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailsPage : ContentPage
    {
        public UserDetailsPage()
        {
            InitializeComponent();
            BindingContext = new UserDetailsViewModel();
        }
    }
}