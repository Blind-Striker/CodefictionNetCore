using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService;

namespace WeatherAppNetFramework
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("Hava durumunu öğrenmek istediğiniz şehir?");
                string city = Console.ReadLine();

                ServiceResponse<WeatherData> serviceResponse = GetCityDataAsync(city).GetAwaiter().GetResult();
                if (serviceResponse.Status == "OK")
                {
                    Console.WriteLine(serviceResponse.Data);
                }
                else
                {
                    Console.WriteLine("Aradığınız şehir bulunamadı!");
                }

                Console.WriteLine("Bir tane daha aramak istiyor musunuz? y/n");
                if (!Console.ReadLine().Equals("y", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }
            }
        }

        public static async Task<ServiceResponse<WeatherData>> GetCityDataAsync(string city)
        {
            OpenWeatherMapService openWeatherMapService = new OpenWeatherMapService();

            ServiceResponse<WeatherData> serviceResponse = await openWeatherMapService.GetWeatherData(city);

            return serviceResponse;
        }
    }
}
