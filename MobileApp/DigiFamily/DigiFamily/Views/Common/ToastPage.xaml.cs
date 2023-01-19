using Rg.Plugins.Popup.Services;
using DigiFamily.Helpers.Utils;
using DigiFamily.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DigiFamily.Views.Common
{
    public partial class ToastPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ToastPage(ToastContent toastContent)
        {
            InitializeComponent();
            _ = App.ToastPage = this;
            if (toastContent != null)
            {
                switch (toastContent.ToastType)
                {
                    case ToastType.Info:
                        Toast.BackgroundColor = Color.FromHex("#f17874");
                        //Title.Text = "Info";
                        Message.Text = toastContent.Message;
                        ToastImage.Source = new FontImageSource() { FontFamily = "FontAwesome", Glyph = IconFont.InformationOutline, Color = Color.White, Size = 25 };
                        break;
                    case ToastType.Success:
                        Toast.BackgroundColor = Color.FromHex("#76d5a4");
                        //Title.Text = "Success";
                        Message.Text = toastContent.Message;
                        ToastImage.Source = new FontImageSource() { FontFamily = "FontAwesome", Glyph = IconFont.CheckCircle, Color = Color.White, Size = 25 };
                        break;
                    case ToastType.Warning:
                        Toast.BackgroundColor = Color.FromHex("#f17874");
                        //Title.Text = "Warning";
                        Message.Text = toastContent.Message;
                        ToastImage.Source = new FontImageSource() { FontFamily = "FontAwesome", Glyph = IconFont.AlertCircleOutline, Color = Color.White, Size = 25 };
                        break;
                    case ToastType.Error:
                        Toast.BackgroundColor = Color.FromHex("#f17874");
                        //Title.Text = "Error";
                        Message.Text = toastContent.Message;
                        ToastImage.Source = new FontImageSource() { FontFamily = "FontAwesome", Glyph = IconFont.AlertOutline, Color = Color.White, Size = 25 };
                        break;
                    default:
                        Toast.BackgroundColor = Color.FromHex("#f17874");
                        //Title.Text = "Info";
                        Message.Text = toastContent.Message;
                        ToastImage.Source = new FontImageSource() { FontFamily = "FontAwesome", Glyph = IconFont.InformationOutline, Color = Color.White, Size = 25 };
                        break;
                }
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.ToastPage = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            HidePopup();
        }
        private async void HidePopup()
        {
            await Task.Delay(4000);
            App.ToastPage = null;
            if (PopupNavigation.Instance.PopupStack.Contains(this))
                await PopupNavigation.Instance.RemovePageAsync(this);
        }

        private async void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            App.ToastPage = null;
            if (PopupNavigation.Instance.PopupStack.Contains(this))
                await PopupNavigation.Instance.RemovePageAsync(this);
        }
    }
}