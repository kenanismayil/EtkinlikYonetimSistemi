using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class AuthorizationModel 
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
