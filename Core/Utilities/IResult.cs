using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public interface IResult
    {
        //Business katmanindaki void methodlar icin field'lar tanimladim.
        bool Success { get; }
        string Message { get; }
    }
}
