using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherReport.Common;
using WeatherReport.Interface;
using WeatherReport.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherReport.ViewModel
{
    public class LocationViewModel : BaseViewModel
    {
        public Command<Location> SelectItem { get; }
        public Command btnRefreshCommand { get; }
        private readonly ICommon _common;
        public bool _isloading;
        public bool _isvisible;
        public List<Locations> _locationslist;

        public bool  IsLoading
        {
            get => _isloading; 
            set { _isloading = value;
                OnPropertyChanged("IsLoading");
            } 
        }
        public bool IsVisible
        {
            get => _isvisible;
            set
            {
                _isvisible = value;
                OnPropertyChanged("IsVisible");
            }
        }
        public List<Locations> LocationList
        {
            get => _locationslist;
            set => SetProperty(ref _locationslist, value);
           

        }
        public LocationViewModel(ICommon common)
        {
            _common = common;
            btnRefreshCommand = new Command(RefreshBtnClick);   
        }

        private   void RefreshBtnClick(object obj)
        {
            IsLoading = true;
            IsVisible = true;
            _common.loadLocation();
            LocationList = DataAccessObject.GetAsync().Result;
            IsLoading = false;
            IsVisible = false;

        }

        private Locations _selectedLocation;
        public Locations SelectedLocation
        {
            get
            {
                return _selectedLocation;
            }
            set
            {
                SetProperty(ref _selectedLocation, value);
                if(_selectedLocation != null)
                { WoeidText = _selectedLocation.woeid;
                    LocationText = _selectedLocation.title;
                }
            }
        }
        private string _woeidTextText;
        public string WoeidText
        {
            get
            {
                return _woeidTextText;
            }
            set
            {
                SetProperty(ref _woeidTextText, value);
            }
        }
        private string _locationText;
        public string LocationText
        {
            get
            {
                return _locationText;
            }
            set
            {
                SetProperty(ref _locationText, value);
            }
        }
    }
}
