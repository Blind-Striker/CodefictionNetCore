using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherService
{
    public class OpenWeatherMapService
    {
        private const string ApiKey = "4fb51fdd05c583081959566ead5fedee";
        private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather";

        public async Task<ServiceResponse<WeatherData>> GetWeatherData(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"{ApiUrl}?q={city}&units=metric&APPID={ApiKey}";
                HttpResponseMessage httpResponseMessage = await client.GetAsync(new Uri(url));

                ServiceResponse<WeatherData> serviceResponse = new ServiceResponse<WeatherData>();

                if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    serviceResponse.Status = "NotOk";
                    return serviceResponse;
                }

                string jsonData = await httpResponseMessage.Content.ReadAsStringAsync();

                WeatherData deserializeObject = JsonConvert.DeserializeObject<WeatherData>(jsonData);

                serviceResponse.Data = deserializeObject;
                serviceResponse.Status = "OK";

                return serviceResponse;
            }
        }
    }

    public class ServiceResponse<TData>
    {
        public TData Data { get; set; }

        public string Status { get; set; }
    }

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double sea_level { get; set; }
        public double grnd_level { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Rain
    {
        public double __invalid_name__3h { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class WeatherData
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Şehir : {name}");
            sb.AppendLine($"Sıcalık : {main.temp}");
            sb.AppendLine($"Nem : {main.humidity}");
            sb.AppendLine($"Sıcalık Max : {main.temp_max}");
            sb.AppendLine($"Sıcalık Min : {main.temp_min}");
            sb.AppendLine($"Açıklama : {weather[0].description}");

            return sb.ToString();
        }
    }
}
