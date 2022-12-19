using Core.Entities.Concrete;
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
    public interface IActivityService
    {
        IDataResult<List<ActivityForView>> GetAll();
        IDataResult<ActivityForView> GetById(int activityId);
        //IDataResult<List<User>> GetParticipiants(int activityId);
        //IDataResult<int> GetMaxUserCount(int activityId);
        IResult Add(ActivityCreatingByAdmin activity);
        IResult Delete(string activityId);
        IResult DeleteAll(Expression<Func<Activity, bool>> filter);
        IResult Update(ActivityCreatingByAdmin activity);
    }
}
