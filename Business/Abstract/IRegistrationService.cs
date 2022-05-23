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
    public interface IRegistrationService
    {
        IDataResult<List<Registration>> GetAll();
        IDataResult<Registration> GetById(int id);
        IResult Add(Registration registration);
        IResult Delete(Registration registration);
        IResult DeleteAll(Expression<Func<Registration, bool>> filter);
        IResult Update(Registration registration);
    }
}
