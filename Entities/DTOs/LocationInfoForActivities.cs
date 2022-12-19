using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class LocationInfoForActivities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
