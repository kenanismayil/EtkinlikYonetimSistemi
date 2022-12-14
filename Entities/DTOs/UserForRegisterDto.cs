using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.DTOs
{
    public class UserForRegisterDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //public RoleType RoleType { get; set; }

    }
}
