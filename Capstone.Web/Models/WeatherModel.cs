using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Extensions;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Models
{
    public class WeatherModel
    {
        public string ParkCode { get; set; }

        public int ForecastDay { get; set; }

        public bool IsFarenheit { get; set; } 

        public string Forecast { get; set; }

        public WeatherModel()
        {
            this.IsFarenheit = true;
        }

        public int Low { get; set; }

        public int DisplayLow
        {
            get
            {
                if(IsFarenheit)
                {
                    return Low;
                }
                else
                {
                    return (int)((Low - 32) / 1.8);
                }
            }
        }


        public int High { get; set; }

        public int DisplayHigh
        {
            get
            {
                if (IsFarenheit)
                {
                    return High;
                }
                else
                {
                    return (int)((High - 32) / 1.8);
                }
            }
        }


        public int ConvertTemp(int Temp)
        {            
            return (int)((Temp - 32) / 1.8);
        }


        public string ForecastMessage
        {
            get
            {
                string message = "";
                if (Forecast == "snow")
                {
                    message = "Pack snow shoes";
                }
                else if (Forecast == "rain")
                {
                    message = "Pack rain gear and wear waterproof shoes.";
                }
                else if (Forecast == "thunderstorms")
                {
                    message = "Seek shelter and avoid hiking on exposed ridges";
                }
                else if (Forecast == "sunny")
                {
                    message = "Pack sun block";
                }
                return message;
            }
        }

       
        public string TemperatureMessage
        {
            get
            {
                string message = "";
                if (High > 75)
                {
                    message = "Bring an extra gallon of water";
                }
                if (High - Low > 20)
                {
                    message += " Wear breathable clothes";
                }
                if (Low < 20)
                {
                    message += " Beware of exposure to frigid temperatures";
                }
                return message;
            }
        }
    }
}
