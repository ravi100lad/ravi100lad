using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WeatherReport.Interface
{
    public interface ISQlite
    {
        SQLiteAsyncConnection GetSqlLiteConnection();
     
    }
}
