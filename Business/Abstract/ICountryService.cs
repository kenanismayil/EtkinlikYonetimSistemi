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
    public interface ICountryService
    {
        IDataResult<List<Country>> GetAll();
        IDataResult<Country> GetById(int id);
        IResult Add(Country country);
        IResult Delete(Country country);
        IResult DeleteAll(Expression<Func<Country, bool>> filter);
        IResult Update(Country country);
    }
}
