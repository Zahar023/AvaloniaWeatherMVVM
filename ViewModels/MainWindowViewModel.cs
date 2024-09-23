using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AvaloniaWeatherMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string url;

        private int _finder = 0;
        
        private string _name = "Moscow";
        private string _pastName = "Moscow";

        private string _humidity;
        private string _pressure;
        private string _speed;

        private string _imageSourceWeb = "https://openweathermap.org/img/wn/@2x.png";
        private string _imageSourceWeb1 = "https://openweathermap.org/img/wn/@2x.png";
        private string _imageSourceWeb2 = "https://openweathermap.org/img/wn/@2x.png";
        private string _imageSourceWeb3 = "https://openweathermap.org/img/wn/@2x.png";
        private string _imageSourceWeb4 = "https://openweathermap.org/img/wn/@2x.png"; 

        private string _time1 = "time";
        private string _time2 = "time";
        private string _time3 = "time";
        private string _time4 = "time";

        private string _date = "date";
        private string _date1 = "date";
        private string _date2 = "date";
        private string _date3 = "date";
        private string _date4 = "date";

        private string _temp1 = "temp";
        private string _temp2 = "temp";
        private string _temp3 = "temp";
        private string _temp4 = "temp";
        private string _temp = "temp";

        public TMP day = null;
        public List<string> Listt = new List<string>();

        public string Speed
        {
            get => _speed;
            set => this.RaiseAndSetIfChanged(ref _speed, value);
        }
        public string Humidity
        {
            get => _humidity;
            set => this.RaiseAndSetIfChanged(ref _humidity, value);
        }
        public string Pressure
        {
            get => _pressure;
            set => this.RaiseAndSetIfChanged(ref _pressure, value);
        }
        public string Name
        {
            get => _name;
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);

                if (_finder == 0)
                {
                    Parse();
                }
            }
        }
        public string Temp1
        {
            get => _temp1;
            set => this.RaiseAndSetIfChanged(ref _temp1, value);
        }
        public string Temp2
        {
            get => _temp2;
            set => this.RaiseAndSetIfChanged(ref _temp2, value);
        }
        public string Temp3
        {
            get => _temp3;
            set => this.RaiseAndSetIfChanged(ref _temp3, value);
        }
        public string Temp4
        {
            get => _temp4;
            set => this.RaiseAndSetIfChanged(ref _temp4, value);
        }
        public string Time1 { 
            get => _time1;  
            set => this.RaiseAndSetIfChanged(ref _time1, value);  
        }
        public string Time2
        {
            get => _time2;
            set => this.RaiseAndSetIfChanged(ref _time2, value);
        }
        public string Time3
        {
            get => _time3;
            set => this.RaiseAndSetIfChanged(ref _time3, value);
        }
        public string Time4
        {
            get => _time4;
            set => this.RaiseAndSetIfChanged(ref _time4, value);
        }

        public string Date
        {
            get => _date;
            set => _date = this.RaiseAndSetIfChanged(ref _date, value);
        }
        public string Date1
        {
            get => _date1;
            set => _date1 = this.RaiseAndSetIfChanged(ref _date1, value);
        }
        public string Date2
        {
            get => _date2;
            set => _date2 = this.RaiseAndSetIfChanged(ref _date2, value);
        }
        public string Date3
        {
            get => _date3;
            set => _date3 = this.RaiseAndSetIfChanged(ref _date3, value);
        }
        public string Date4
        {
            get => _date4;
            set => _date4 = this.RaiseAndSetIfChanged(ref _date4, value);
        }

        public string ImageSourceWeb
        {
            get =>_imageSourceWeb;
            set => _imageSourceWeb = this.RaiseAndSetIfChanged(ref _imageSourceWeb, value);         
        }
        public string ImageSourceWeb1
        {
            get => _imageSourceWeb1;
            set => _imageSourceWeb1 = this.RaiseAndSetIfChanged(ref _imageSourceWeb1, value);
        }
        public string ImageSourceWeb2
        {
            get => _imageSourceWeb2;
            set => _imageSourceWeb2 = this.RaiseAndSetIfChanged(ref _imageSourceWeb2, value);
        }
        public string ImageSourceWeb3
        {
            get => _imageSourceWeb3;
            set => _imageSourceWeb3 = this.RaiseAndSetIfChanged(ref _imageSourceWeb3, value);
        }
        public string ImageSourceWeb4
        {
            get => _imageSourceWeb4;
            set => _imageSourceWeb4 = this.RaiseAndSetIfChanged(ref _imageSourceWeb4, value);
        }
        public ObservableCollection<string> Tempe { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Time { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ImageSource { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<TMP> Days { get; set; } = new ObservableCollection<TMP>();

        private List<TMP> _days = new();
        public List<TMP> Days2 { get => _days; set => this.RaiseAndSetIfChanged(ref _days, value); }


        public string Temp
        { 
            get => _temp; 
            set => this.RaiseAndSetIfChanged(ref _temp, value);   
        }

        public MainWindowViewModel()
        {
            Parse();
        }
        private List<MainInfo> _list = new();
        public List<MainInfo> list
        {
            get
            {
                return _list;
            }
            set { _list = this.RaiseAndSetIfChanged(ref _list ,value); }
        }

        public async Task Parse()
        {
            
            WeatherResponse weatherResponse = await Response(Name);
            if (weatherResponse != null)
            {
                _finder = 1;
                if (day != null)
                {
                    day = null;
                }
                for (int i = 0; i < 5; i++)
                {
                    Tempe.Add((((int)weatherResponse.List[i].Main.Temp) - 273).ToString() + "°");

                    list.Add(weatherResponse.List[i].Main);

                    day = new(weatherResponse.List[i * 8].dt_txt, (((int)weatherResponse.List[i * 8].Main.Temp) - 273).ToString() + "°", ImageSourceWeb.Insert(34, weatherResponse.List[i * 8].Weather[0].Icon));
                    Days.Add(day);
                }

                Name = weatherResponse.City.Name;

                Humidity = weatherResponse.List[0].Main.Humidity.ToString() + "%";
                Pressure = weatherResponse.List[0].Main.Pressure.ToString() + " mm";
                Speed = weatherResponse.List[0].Wind.Speed.ToString() + " m/s";

                ImageSourceWeb = ImageSourceWeb.Insert(34, weatherResponse.List[0].Weather[0].Icon);
                ImageSourceWeb1 = ImageSourceWeb1.Insert(34, weatherResponse.List[1].Weather[0].Icon);
                ImageSourceWeb2 = ImageSourceWeb2.Insert(34, weatherResponse.List[2].Weather[0].Icon);
                ImageSourceWeb3 = ImageSourceWeb3.Insert(34, weatherResponse.List[3].Weather[0].Icon);
                ImageSourceWeb4 = ImageSourceWeb4.Insert(34, weatherResponse.List[4].Weather[0].Icon);

                Temp = (((int)weatherResponse.List[0].Main.Temp) - 273).ToString() + "°";
                Temp1 = (((int)weatherResponse.List[1].Main.Temp) - 273).ToString() + "°";
                Temp2 = (((int)weatherResponse.List[2].Main.Temp) - 273).ToString() + "°";
                Temp3 = (((int)weatherResponse.List[3].Main.Temp) - 273).ToString() + "°";
                Temp4 = (((int)weatherResponse.List[4].Main.Temp) - 273).ToString() + "°";

                Time1 = weatherResponse.List[1].dt_txt;
                Time1 = Time1.Remove(0, 11);
                Time1 = Time1.Remove(4, 3);
                Time2 = weatherResponse.List[2].dt_txt;
                Time2 = Time2.Remove(0, 11);
                Time2 = Time2.Remove(4, 3);
                Time3 = weatherResponse.List[3].dt_txt;
                Time3 = Time3.Remove(0, 11);
                Time3 = Time3.Remove(4, 3);
                Time4 = weatherResponse.List[4].dt_txt;
                Time4 = Time4.Remove(0, 11);
                Time4 = Time4.Remove(4, 3);
            }
            else
            {
                _finder = 0;
            }
        }

        static async Task<WeatherResponse> Response(string name)
        {
            string apiUrl = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&appid=4db484a711345088e10096314e2c83af", name);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    // Обработка полученного JSON-ответа
                    WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(json);
                    return weatherResponse;
                }
                else
                {
                    Console.WriteLine("Ошибка при выполнении запроса");
                } 
            }
            return null;
        }

        public void GetResp()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
            //_temperature = weatherResponse.Main.Temp.ToString();

        }
    }

    public class TMP
    {
        public string DateTMP { get; set; } 
        public string TempTMP {  get; set; }
        public string ImageTMP {  get; set; }
        public TMP(string date, string temp, string image)
        {
            date = date.Remove(0, 5);
            date = date.Remove(5, 9);
            DateTMP = date;
            TempTMP = temp;
            ImageTMP = image;
        }
    }
}
