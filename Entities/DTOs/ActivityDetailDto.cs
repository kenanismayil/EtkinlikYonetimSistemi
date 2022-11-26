using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ActivityDetailDto : IDto
    {
        public int ActivityId { get; set; }
        //public int UserId { get; set; }
        //public string CommentName { get; set; }
        //public string Attandies { get; set; }
        public string ActivityName { get; set; }
        //public string UserName { get; set; }
        public string ActivityTypeName { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ActivityDate { get; set; }
        public DateTime AppDeadLine { get; set; }
    }
}
