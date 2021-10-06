using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using WeatherReport.DAL;
using WeatherReport.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetSQLiteConnnection))]
namespace WeatherReport.DAL
{
    public class GetSQLiteConnnection : ISQlite
    {
        public SQLiteAsyncConnection GetSqlLiteConnection()
        {
            var dbase = "Mydatabase";
            var dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dbpath, dbase);
            var connection = new SQLiteAsyncConnection(path);
            return connection;
        }

    }
}
