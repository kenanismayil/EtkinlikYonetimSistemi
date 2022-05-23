using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Certificate:IEntity
    {
        public int Id { get; set; }
        public string CertificateName { get; set; }
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public DateTime GivenDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        //Foreign Key
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
    }
}
