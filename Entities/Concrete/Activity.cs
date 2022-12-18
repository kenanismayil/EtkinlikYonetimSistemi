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
        public int? LocationId { get; set; }
        public int? UserId { get; set; }
        public int? ActivityTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Participiant { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime AppDeadLine { get; set; }
        public DateTime ActivityDate { get; set; }

        //Foreign key
        [ForeignKey("ActivityTypeId")]
        public virtual ActivityType ActivityType { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
