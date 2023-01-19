using DigiFamily.Helpers.Utils;
using DigiFamily.Models;
using DigiFamily.Views;
using System;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView.Commands;
using Xamarin.Forms;

namespace DigiFamily.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties
        private string searchTxt;
        public string SearchTxt { get => searchTxt; set => SetProperty(ref searchTxt, value); }
        public Command UserTapCommand { get; }
        public Command AddCommand { get; }
        public Command FamilyHeadCommand { get; }
        #endregion

        public HomeViewModel()
        {
            Title = "Home";
            UserTapCommand = new Command<ItemTapCommandContext>(OnUserTappedAsync);
            AddCommand = new Command(OnAddClickedAsync);
            FamilyHeadCommand = new Command(OnFamilyHeadClickedAsync);
        }

        private async void OnFamilyHeadClickedAsync(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}?userid=12&pagetype={DetailPageType.Update}&isfamilyhead={true}");
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

        private async void OnAddClickedAsync(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}?userid=12&pagetype={DetailPageType.Add}&isfamilyhead={false}");
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

        private async void OnUserTappedAsync(ItemTapCommandContext context)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                if (context != null && context.Item != null)
                {
                    string tappedItem = context.Item as string;
                    // This will push the ItemDetailPage onto the navigation stack
                    await Shell.Current.GoToAsync($"{nameof(UserDetailsPage)}?userid=12&pagetype={DetailPageType.Update}&isfamilyhead={false}");
                }
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
    }
}
