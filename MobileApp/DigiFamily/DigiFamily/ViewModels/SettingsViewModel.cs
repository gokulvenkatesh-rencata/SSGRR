using DigiFamily.Helpers;
using DigiFamily.Helpers.Utils;
using System;
using Telerik.XamarinForms.Primitives.CheckBox.Commands;
using Xamarin.Forms;

namespace DigiFamily.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Properties
        private bool isDarkTheme;

        public bool IsDarkTheme { get => isDarkTheme; set => SetProperty(ref isDarkTheme, value); }
        public Command IsDarkThemeCommand { get; }
        #endregion
        public SettingsViewModel()
        {
            Title = "Settings";
            IsDarkThemeCommand = new Command(OnDarkThemeClicked);
        }


        private void OnDarkThemeClicked(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                if (obj is CheckBoxIsCheckChangedCommandContext context && context.NewState == true)
                    CommonHelper.SetTheme(OSAppTheme.Dark);
                else
                    CommonHelper.SetTheme(OSAppTheme.Light);
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
