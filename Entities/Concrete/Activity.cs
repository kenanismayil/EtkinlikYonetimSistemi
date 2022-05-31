using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Activity:IEntity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public int ActivityTypeId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime AppDeadLine { get; set; }
        public DateTime ActivityDate { get; set; }
        public int CityId { get; set; }
        //public int ModeratorId { get; set; }

        //Foreign key
        [ForeignKey("ActivityTypeId")]
        public ActivityType ActivityType { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        //[ForeignKey("ModeratorId")]
        //public virtual Moderator Moderator { get; set; }
    }
}
