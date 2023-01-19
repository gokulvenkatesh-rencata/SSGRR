using DigiFamily.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiFamily.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private HomeViewModel ViewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new HomeViewModel();
            listView.ItemsSource= new List<string> { "Manjula", "Prathiksha" , "Ridhikaa" };
        }
    }
}