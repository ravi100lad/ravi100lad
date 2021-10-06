using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherReport.Interface;
using WeatherReport.Model;
using Xamarin.Forms;

namespace WeatherReport.BAL
{
    /// <summary>
    /// This is business layer contain code to insert and fetch the record from database.
    /// </summary>
    public class BAlocation : IDataAccess<Locations>
    {
        private SQLiteAsyncConnection conn;
        public BAlocation()
        {
            conn = DependencyService.Get<ISQlite>().GetSqlLiteConnection();
        }
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(List<Locations> item)
        {
           return  await conn.InsertAllAsync(item);
        }
        /// <summary>
        /// Fetch the data
        /// </summary>
        /// <returns></returns>
        public  Task<List<Locations>> GetAsync()
        {
            return  conn.Table<Locations>().ToListAsync();
        }
    }
}
