using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using DigiFamily.Models;
using System;
using System.Collections.Generic;
using System.Web;
using Xamarin.Forms;

namespace DigiFamily.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        #region Properties
        private DetailPageType pagetype;
        private long userId;
        private bool isFamilyHead = false;
        private string buttonCaption;

        public DetailPageType PageType { get => pagetype; set => SetProperty(ref pagetype, value); }
        public long UserId { get => userId; set => SetProperty(ref userId, value); }
        public bool IsFamilyHead { get => isFamilyHead; set => SetProperty(ref isFamilyHead, value); }
        public string ButtonCaption { get => buttonCaption; set => SetProperty(ref buttonCaption, value); }

        public Command SubmitCommand { get; }
        public Command BackPageCommand { get; }
        #endregion

        public UserDetailsViewModel()
        {
            BackPageCommand = new Command(OnBackPage);
            SubmitCommand = new Command(OnSubmitClicked);
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
}
