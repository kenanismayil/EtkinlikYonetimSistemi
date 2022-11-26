using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILocationService
    {
        IDataResult<List<Location>> GetAll();
        IDataResult<Location> GetById(int locationId);
        IResult Add(Location location);
        IResult Delete(Location location);
        IResult Update(Location location);
    }
}
