using Core.Entities;
using Core.Entities.Concrete;
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
        public int LocationId { get; set; }
        public int? UserId { get; set; }
        public int ActivityTypeId { get; set; }
        public string ActivityName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime AppDeadLine { get; set; }
        public DateTime ActivityDate { get; set; }

        //Foreign key
        [ForeignKey("ActivityTypeId")]
        public ActivityType ActivityType { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
