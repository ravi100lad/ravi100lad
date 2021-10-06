using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReport.Common;
using WeatherReport.Interface;
using WeatherReport.Model;
using WeatherReport.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherReport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailView : ContentPage
    {
        public ObservableCollection<consolidated_weather> items;
        private CommonMethods cmobject = new CommonMethods();
        public ObservableCollection<consolidated_weather> ConsolidatedWeatherList
        {
            get { return items; }
            set
            {
                items = value;
            }
        }
        public DetailView(string woeid, string locationtext)
        {
            InitializeComponent();
            this.Title = locationtext;
            ConsolidatedWeatherList = cmobject.GetWeatherData(woeid);
            if(ConsolidatedWeatherList.Count != 0)
            { DetailListView.ItemsSource = ConsolidatedWeatherList; }
            else
            {
                errorlbl.IsVisible = true;
                errorlbl.Text = "Error! Please check the internet connection";
            }
            
        }
    }
}