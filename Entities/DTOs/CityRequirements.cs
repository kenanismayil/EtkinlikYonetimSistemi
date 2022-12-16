using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CityRequirements
    {
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string CityName { get; set; }
    }
}
