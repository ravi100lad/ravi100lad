using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherReport.Model;

namespace WeatherReport.Interface
{
    public interface ICommon
    {
         void loadLocation();
         void GetLocations(double lond, double latd);
        ObservableCollection<consolidated_weather> GetWeatherData(string woeid);
    }

}
