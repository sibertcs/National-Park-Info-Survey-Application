using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Park
    {
        public string ParkCode { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int Acreage { get; set; }

        public int Elevation { get; set; }

        public int TrailMiles { get; set; }

        public int NumberOfCampsites { get; set; }

        public string Climate { get; set; }

        public int YearFounded { get; set; }

        public int AnnualVisitorCount { get; set; }

        public string InspirationalQuote { get; set; }

        public string QuoteSource { get; set; }

        public string Description { get; set; }

        public decimal Fee { get; set; }

        public int NumberOfAnimalSpecies { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
