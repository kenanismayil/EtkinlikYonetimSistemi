using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ActivityImage : IEntity
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
    }
}
