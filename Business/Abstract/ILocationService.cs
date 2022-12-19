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

        IDataResult<List<Location>> GetLocationByCityId(int cityId);
        IResult Add(Location location);
        IResult Delete(string locationId);
        IResult Update(Location location);
    }
}
