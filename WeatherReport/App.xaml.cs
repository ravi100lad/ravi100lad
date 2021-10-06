


using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using SQLite;
using System;
using WeatherReport.BAL;
using WeatherReport.Common;
using WeatherReport.Interface;
using WeatherReport.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherReport
{
    public partial class App : Application
    {
        protected SQLiteAsyncConnection con;
         
        public App()
        {
            InitializeComponent();

            UnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType<ICommon, CommonMethods>();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
            try
            {
                DependencyService.Register<BAlocation>();
                DependencyService.Register<CommonMethods>();
                con = DependencyService.Get<ISQlite>().GetSqlLiteConnection();
                con.CreateTableAsync<Locations>();   
            }
            catch (Exception ex)
            {
                throw ex;
            }
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
