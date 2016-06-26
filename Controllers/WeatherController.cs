using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExamWeather.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private Random rng = new Random();

        // GET api/weather/Chelyabinsk
        [HttpGet("{cityName}")]
        public IActionResult Get(string cityName)
        {
            var now = DateTime.Now;
            var items = Enumerable
                            .Range(0, 7)
                            .Select((i) => now.Date.AddDays(i))
                            .Select((day) => {
                                var temperature = rng.NextDouble() * 50 - 25;
                                var humidity = rng.NextDouble();
                                return new {
                                    date = String.Format("{0:yyyy-MM-dd}", day),
                                    temperature = temperature,
                                    humidity = humidity,
                                    text = String.Format("Температура: {0}. Влажность: {1}%. Будет солнечно.", temperature, Math.Round(humidity * 100))
                                };
                            });
            return new ObjectResult(new {
                city = cityName,
                time = now,
                items = items
            });
        }
    }
}
