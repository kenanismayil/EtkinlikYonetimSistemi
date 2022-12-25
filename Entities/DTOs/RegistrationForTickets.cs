using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RegistrationForTickets : IDto
    {
        public int ActivityId { get; set; }
        public string ActivityTitle { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityImage { get; set; }
        public string PnrNo { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
