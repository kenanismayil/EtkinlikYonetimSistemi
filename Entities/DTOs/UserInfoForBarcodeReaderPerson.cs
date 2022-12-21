using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserInfoForBarcodeReaderPerson
    {
        public int Id { get; set; }
        public UserInfoForActivities User { get; set; }
        public bool isUserOnEventPlace { get; set; }
    }
}
