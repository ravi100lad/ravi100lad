using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WeatherReport.Interface;
using WeatherReport.Model;
using Xamarin.Forms;

namespace WeatherReport.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public  IDataAccess<Locations> DataAccessObject => DependencyService.Get<IDataAccess<Locations>>();
        public event PropertyChangedEventHandler PropertyChanged;
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T property, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
                return false;

            property = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                var changed = PropertyChanged;
                if (changed == null)
                    return;

                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
