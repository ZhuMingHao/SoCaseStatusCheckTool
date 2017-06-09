using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using SoCaseStatusCheck.Core.Https;
using SoCaseStatusCheck.Core.Models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace SoCaseStatusCheckTool.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            GetSoCasesData();
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        private APIService _api = new APIService();
        private SoCase SelectItem;

        private ObservableCollection<SoCase> _cases;

        public ObservableCollection<SoCase> Cases
        {
            get
            {
                return _cases;
            }
            set
            {
                Set(ref _cases, value);

            }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                Set(ref _selectedIndex, value);
            }
        }
        private bool _isloading;
        public bool IsLoading
        {
            get
            {
                return _isloading;
            }
            set
            {
                Set(ref _isloading, value);

            }
        }

        private string _alias;
        public string Alias
        {
            get { return _alias; }
            set { Set(ref _alias, value); }
        }
        private DateTime _startTime;
        public DateTime StartTime { get { return _startTime; } set { Set(ref _startTime, value); } }
        private DateTime _endTime;
        public DateTime EndTime { get { return _endTime; } set { Set(ref _endTime, value); } }

        private async void GetSoCasesData()
        {
            IsLoading = true;

            List<SoCase> list = await _api.GetThemes();

            if (list != null)
            {
                Cases = new ObservableCollection<SoCase>();
                list.ForEach(
                    (temp) => { Cases.Add(temp); });
                SelectedIndex = 0;
            }
            await Task.Delay(5000);
            IsLoading = false;

        }

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        public void ShowTheMenu(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            var listview = sender as ListView;
            MenuFlyout myFlyout = new MenuFlyout();
            MenuFlyoutItem firstItem = new MenuFlyoutItem { Text = "Copy ID" };
            MenuFlyoutItem secondItem = new MenuFlyoutItem { Text = "Copy URL" };
            MenuFlyoutItem thirdItem = new MenuFlyoutItem { Text = "Open in the browser" };
            firstItem.Click += FirstItem_Click;
            secondItem.Click += SecondItem_Click;
            thirdItem.Click += ThirdItem_Click;
            myFlyout.Items.Add(firstItem);
            myFlyout.Items.Add(secondItem);
            myFlyout.Items.Add(thirdItem);
            SelectItem = ((FrameworkElement)e.OriginalSource).DataContext as SoCase;
            myFlyout.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));

        }

        private void ThirdItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SecondItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FirstItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
