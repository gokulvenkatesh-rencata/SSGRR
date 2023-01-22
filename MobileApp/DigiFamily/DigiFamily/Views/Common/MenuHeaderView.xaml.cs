using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiFamily.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuHeaderView : ContentView
    {
        public MenuHeaderView()
        {
            InitializeComponent();
            MessagingCenter.Unsubscribe<Application>(this, "UpdateProfile");
            MessagingCenter.Subscribe<Application>(this, "UpdateProfile", async (sender) =>
             {
                 await UpdateProfileAsync();
             });
            MessagingCenter.Send<Application>(Application.Current, "UpdateProfile");
        }
        async Task UpdateProfileAsync()
        {
            string username = await SecureStorage.GetAsync("oauth_username");
            if (!string.IsNullOrEmpty(username))
            {
                UsernameLabel.Text = "Sasindran Thavaselvam";
                //ProfilePic.Source = new UriImageSource().Uri = new System.Uri("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTxv6KzSo-T89N0xfuVNpO--yIlXba83JFrxUcJ0SuocPbbjpK");
                ProfilePic.Source = new FontImageSource
                {
                    FontFamily = "FontAwesome",
                    Glyph = IconFont.Account,
                    Color = CommonHelper.GetResource<Color>("IconColor"),
                    Size = 50
                };
            }
        }
    }
}