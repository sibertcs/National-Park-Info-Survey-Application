using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Capstone.Web.Extensions;
using System.Net.Http;
using Newtonsoft.Json;
using static Capstone.Web.Models.WeatherApiModel;
using System.Collections;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO dao;

        private IWeatherDAO weatherDAO;

        public HomeController(IParkDAO dao, IWeatherDAO weatherDAO)
        {
            this.dao = dao;
            this.weatherDAO = weatherDAO;
        }
        public IActionResult Index()
        {
            IList<Park> parks = dao.GetParks();
            return View(parks);
        }



        [HttpGet]
        public async Task<ActionResult> Detail(string parkCode)
        {
            DetailViewModel model = new DetailViewModel();

            model.Park = dao.GetParkInfo(parkCode);
            //model.Weather = weatherDAO.GetWeather(parkCode);

            string latitude = model.Park.Latitude.ToString();
            string longitude = model.Park.Longitude.ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.darksky.net/forecast/4444e8d39107c165b387134476fb4467/");

                //HTTP GET
                var responseTask = client.GetAsync(latitude + "," + longitude + "?exclude=currently,minutely,hourly,alerts,flags");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string content = await result.Content.ReadAsStringAsync();
                    var weatherArray = JsonConvert.DeserializeObject<Rootobject>(content).daily.data;
                    for (int i = 0; i < 5; i++)
                    {
                        WeatherModel wm = new WeatherModel();
                        wm.High = (int)weatherArray[i].temperatureHigh;
                        wm.Low = (int)weatherArray[i].temperatureLow;
                        wm.Forecast = weatherArray[i].icon;
                        wm.ForecastDay = i + 1;
                        wm.ParkCode = parkCode;
                        model.Weather.Add(wm);
                    }
                }

                bool isFarenheit = HttpContext.Session.Get<bool>("isF");

                if (HttpContext.Session.Keys.Contains("isF") == false)
                {
                    HttpContext.Session.Set("isF", true);
                }


                foreach (WeatherModel weather in model.Weather)
                {
                    weather.IsFarenheit = HttpContext.Session.Get<bool>("isF");
                }

                bool isF = HttpContext.Session.Get<bool>("isF");
                ViewData["isF"] = isF;
            }
            //ViewData["image"] = url;

            return View(model);
        }

        










        public IActionResult ChangeTempType(string parkCode)
        {
            
            bool isFarenheit = HttpContext.Session.Get<bool>("isF");
            HttpContext.Session.Set("isF", !isFarenheit);
            return RedirectToAction("Detail", "Home", new { parkCode = parkCode });
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
