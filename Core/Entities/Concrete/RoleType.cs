﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class RoleType : IEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
