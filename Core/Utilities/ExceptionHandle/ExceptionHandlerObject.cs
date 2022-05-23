﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.ExceptionHandle
{
    public class ExceptionHandlerObject<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
