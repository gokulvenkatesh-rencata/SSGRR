using Xamarin.Forms.Xaml;

namespace DigiFamily.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverlayLoadingPopupPage: Rg.Plugins.Popup.Pages.PopupPage
    {
        public OverlayLoadingPopupPage()
        {
            InitializeComponent();
        }
    }
}