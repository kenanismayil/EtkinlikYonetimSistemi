using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    //burda 1-n ilişki var, aslında n-n ilişki olması gerekiyor. Ayrıca tablo oluşturup useropertaionclaimId gibi yapmak lazım
    public class UserDetailDto : IEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserPhoto { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public int ActivityTypeId { get; set; }
        public string ActivityTypeName { get; set; }

    }
}
