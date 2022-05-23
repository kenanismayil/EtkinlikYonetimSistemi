using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Moderator:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        //Foreign key
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
