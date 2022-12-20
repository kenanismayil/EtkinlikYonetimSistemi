using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ActivityCreatingByAdmin
    {
        public int Id { get; set; }
        public int ActivityTypeId { get; set; }
        public int LocationId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Participiant { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
