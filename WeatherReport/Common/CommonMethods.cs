using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherReport.Interface;
using WeatherReport.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherReport.Common
{
    public class CommonMethods : ICommon
    {
        protected SQLiteAsyncConnection conn;
        public double latd;
        public double lond;
        private static Placemark placemark;
        public CommonMethods()
        {
            conn = DependencyService.Get<ISQlite>().GetSqlLiteConnection();
        }

        /// <summary>
        /// Get the location
        /// </summary>
        public async void loadLocation()
        {
            try
            {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                var plcemrks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                placemark = plcemrks.FirstOrDefault();
                Xamarin.Forms.Application.Current.Properties["plc"] = placemark;

                if (Application.Current.Properties.ContainsKey("plc"))
                {
                    var placemark = (Placemark)Application.Current.Properties["plc"];
                    //    loadLocation();
                    latd = placemark.Location.Latitude;
                    lond = placemark.Location.Longitude;
                    GetLocations(lond, latd);
                }
            }
        }
         catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", "GPS Signal Not Found", "Ok");
            }

        }


        /// <summary>
        /// Find location from https://www.metaweather.com/api/location/ API on basis of
        /// Lattitude and Longitude of the device.
        /// </summary>
        /// <param name="lond"></param>
        /// <param name="latd"></param>
        public async void GetLocations(double lond, double latd)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format("{0}={1},{2}", Constants.locaionsrchurl, latd, lond));
            if (response != null)
            {
                List<Locations> locationObjs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Locations>>(await response.Content.ReadAsStringAsync());
                await conn.DeleteAllAsync<Locations>();
                await conn.InsertAllAsync(locationObjs); 
            }

        }

        /// <summary>
        ///  Get Weather States of selected location from dropdown list
        /// </summary>
        /// <param name="woeid"></param>
        /// <returns></returns>
        public ObservableCollection<consolidated_weather> GetWeatherData(string woeid)
        {
            ObservableCollection<consolidated_weather> consweterObjs = new ObservableCollection<consolidated_weather>();
            try
            {
                HttpClient client = new HttpClient();
                var response = client.GetStringAsync(string.Format("{0}{1}/", Constants.locationurl, woeid));
                if (response != null)
                {
                        var data = new List<string>();
                        using (var document = JsonDocument.Parse(response.Result))
                        {
                            JsonElement root = document.RootElement;
                            Rotate(root);
                            void Rotate(JsonElement element)
                            {
                                if (element.ValueKind == JsonValueKind.Object)
                                {
                                    foreach (var item in element.EnumerateObject())
                                    {
                                        data.Add(item.Value.GetRawText());
                                        break;
                                    }
                                }
                            }
                        }
                        JArray array = JArray.Parse(data[0]);
                        foreach (JObject obj in array.Children<JObject>())
                        {
                            consolidated_weather cwobj = new consolidated_weather();
                            cwobj.max_temp = "Max Temp: " + Math.Round(Convert.ToDecimal(obj.Property("max_temp").Value)) + " C";
                            cwobj.min_temp = "Min Temp: " + Math.Round(Convert.ToDecimal(obj.Property("min_temp").Value)) + " C";
                            DateTime date = DateTime.Parse(obj.Property("applicable_date").Value.ToString());

                            cwobj.applicable_date = string.Format("{0:ddd, MMM d, yyyy}", date);
                            cwobj.weather_state_name = Convert.ToString(obj.Property("weather_state_name").Value);
                            cwobj.iconurl = Convert.ToString(obj.Property("weather_state_abbr").Value) + ".png";
                            consweterObjs.Add(cwobj);
                        }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return consweterObjs;
        }
    }
}
