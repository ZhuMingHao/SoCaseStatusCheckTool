using SoCaseStatusCheck.Core.Https;
using SoCaseStatusCheck.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCaseStatusCheck.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        APIService _api = new APIService();
        public MainViewModel()
        {
            GetSoCasesData();
        }
        private ObservableCollection<SoCase> _cases;
        public ObservableCollection<SoCase> Cases
        {
            get
            {
                return _cases;
            }
            set
            {
                _cases = value;
                OnPropertyChanged();
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
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }
        private bool _is_loading;
        public bool IsLoading
        {
            get
            {
                return _is_loading;
            }
            set
            {
                _is_loading = value;
                OnPropertyChanged();
            }
        }

        private async void GetSoCasesData()
        {
            IsLoading = true;
            List<SoCase> list = await _api.GetThemes();
            if (list != null)
            {
                Cases = new ObservableCollection<SoCase>();
                ///
                list.ForEach(
                    (temp) => { Cases.Add(temp); });
                SelectedIndex = 0;
            }
            IsLoading = false;

        }

    }
}
