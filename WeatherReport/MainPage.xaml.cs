using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherReport
{
    public partial class MainPage : ContentPage
    {
        public  MainPage()
        {
            InitializeComponent();

            //Check Internet Connection Of Device At Here.
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                if (Application.Current.Properties.ContainsKey("woeid"))
                {
                    var woeid = Application.Current.Properties["woeid"];
                }
                
            }
            else {
                //Disable the controls at here
                ddllocation.IsEnabled = false;
                btnrefresh.IsEnabled = false;
                errorlbl.IsVisible = true;
                errorlbl.Text = "Internet Is Not Available";
            }
        }
        private  void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IsEnable();
                Application.Current.Properties["woeid"] = txtwoeid.Text;
                Navigation.PushAsync(new DetailView(txtwoeid.Text,txtlocationname.Text));
                IsDisable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void IsEnable()
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
        }
        public void IsDisable()
        {

            activity.IsEnabled = false;
            activity.IsRunning = false;
            activity.IsVisible = false;
        }

      
    }
}
