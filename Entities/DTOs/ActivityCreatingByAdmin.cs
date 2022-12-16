using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ActivityCreatingByAdmin
    {
        public int UserId { get; set; }
        public int ActivityTypeId { get; set; }
        public int LocationId { get; set; }
        public string ActivityName { get; set; }
        public int Participiant { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime AppDeadLine { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
