using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.DependencyResolvers.Modules.Abstract
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
