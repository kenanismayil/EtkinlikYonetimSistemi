using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class City : IEntity
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string CityName { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}
