using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class LocationRequirements
    {
        public int LocationId { get; set; }
        public int? CityId { get; set; }
        public string LocationName { get; set; }
    }
}
