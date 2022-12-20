using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.DTOs
{
    public class UserRegisterInfo : IDto
    {
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public bool IsRegistered { get; set; }


        //public RoleType RoleType { get; set; }

    }
}
