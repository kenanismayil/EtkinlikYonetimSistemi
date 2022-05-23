using Core.Utilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICityService
    {
        IDataResult<List<City>> GetAll();
        IDataResult<City> GetById(int id);
        IResult Add(City city);
        IResult Delete(City city);
        IResult DeleteAll(Expression<Func<City, bool>> filter);
        IResult Update(City city);
    }
}
