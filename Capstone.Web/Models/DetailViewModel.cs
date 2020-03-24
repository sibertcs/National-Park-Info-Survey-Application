﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class DetailViewModel
    {
        public Park Park { get; set; }

        public IList<WeatherModel> Weather { get; set; } = new List<WeatherModel>();
    }
}
