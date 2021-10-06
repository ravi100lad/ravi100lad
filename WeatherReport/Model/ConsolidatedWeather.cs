using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WeatherReport.Model
{
    public class consolidated_weather
    {
        public string weather_state_name { get; set; }
        public string applicable_date { get; set; }
        public string min_temp { get; set; }
        public string max_temp { get; set; }
        public string weather_state_abbr { get; set; }

        public string iconurl { get; set; }

    }
}
