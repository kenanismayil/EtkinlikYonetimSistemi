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
        IDataResult<List<Activity>> GetAll();
        IDataResult<Activity> GetById(int activityId);
        IDataResult<List<ActivityDetailDto>> GetActivityDetails();
        IResult Add(Activity activity);
        IResult Delete(Activity activity);
        IResult DeleteAll(Expression<Func<Activity, bool>> filter);
        IResult Update(Activity activity);
        //IResult AddTransactionalTest(Activity activity);
    }
}
