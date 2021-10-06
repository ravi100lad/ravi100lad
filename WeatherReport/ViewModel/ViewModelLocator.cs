using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherReport.ViewModel
{
    public class ViewModelLocator
    {
        public LocationViewModel locationViewModel
        {
          get
            {
                return ServiceLocator.Current.GetInstance<LocationViewModel>();
            }
        } 
    }
}
