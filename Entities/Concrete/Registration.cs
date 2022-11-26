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
    public class Registration : IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int ActivityId { get; set; }
        public DateTime Date { get; set; }

        //Foreign key
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
    }
}
