using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
        IDataResult<List<Registration>> GetAllForFilter(Expression<Func<Registration, bool>> filter);
        IDataResult<Registration> GetById(int id);
        IResult Add(RegisterForActivity registration);
        IResult Delete(RegisterForActivity registration);
        //IResult DeleteAll(Expression<Func<Registration, bool>> filter);
        IResult Update(RegisterForActivity registration);
    }
}
