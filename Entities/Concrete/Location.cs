using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Location : IEntity
    {
        public int Id { get; set; }
        public int? CityId { get; set; }
        public string Name { get; set; }


        //Foreign Key
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
    }
}
