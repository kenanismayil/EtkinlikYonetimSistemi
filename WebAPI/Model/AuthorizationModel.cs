using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    public class AuthorizationModel 
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UserForView User { get; set; }
    }
}
