using DigiFamily.Models;
using Xamarin.CommunityToolkit.UI.Views;

namespace DigiFamily.Views.Common
{
    public partial class AlertPopupPage : Popup<AlertContent>
    {
        public AlertPopupPage(AlertContent content)
        {
            InitializeComponent();
            App.AlertPopup = this;
            if (content != null)
            {
                Title.Text = content.Title;
                Message.Text = content.Message;
                switch (content.AlertType)
                {
                    case AlertType.Confirm:
                        okBtn.IsVisible = false;
                        ConfirmOptions.IsVisible = true;
                        NoButton.Text = content.NoBtn;
                        YesButton.Text = content.YesBtn;
                        break;
                    case AlertType.Notify:
                    default:
                        ConfirmOptions.IsVisible = false;
                        okBtn.IsVisible = true;
                        okBtn.Text = content.OkBtn;
                        break;
                }
            }
        }

        private void OkButton_Clicked(object sender, System.EventArgs e)
        {
            App.AlertPopup = null;
            Dismiss(null);
        }

        private void NoButton_Clicked(object sender, System.EventArgs e)
        {
            App.AlertPopup = null;
            AlertContent displayAlertPopUpResult = new AlertContent()
            {
                IsConfirmed = false
            };
            Dismiss(displayAlertPopUpResult);
        }

        private void YesButton_Clicked(object sender, System.EventArgs e)
        {
            App.AlertPopup = null;
            AlertContent displayAlertPopUpResult = new AlertContent()
            {
                IsConfirmed = true
            };
            Dismiss(displayAlertPopUpResult);
        }
    }
}