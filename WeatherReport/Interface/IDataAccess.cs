using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReport.Interface
{
    public interface IDataAccess<T>
    {
        Task<int> AddAsync(List<T> item);
      
        Task<List<T>> GetAsync();
    }
}
