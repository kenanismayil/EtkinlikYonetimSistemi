using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class ActivityForView
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }
        public int Participiant { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ActivityDate { get; set; }
        public LocationInfoForActivities Location { get; set; }
        public ActivityType ActivityType { get; set; }
        public UserInfoForActivities User { get; set; }
    }
}
