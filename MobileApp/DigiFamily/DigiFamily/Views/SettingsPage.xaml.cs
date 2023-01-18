using DigiFamily.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiFamily.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel ViewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SettingsViewModel();
        }
    }
}