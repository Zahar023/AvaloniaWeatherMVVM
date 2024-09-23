using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaWeatherMVVM.ViewModels
{
    public class WeatherResponse
    {
        public City City { get; set; }
        public List<list> List {  get; set; }
    }
    public class Weather
    {
        public string Icon { get; set; }    
    }
    public class list
    {
        public List<Weather> Weather { get; set; }
        public string dt_txt {  get; set; }
        public MainInfo Main { get; set; }
        public WindInfo Wind { get; set; }

    }
}
