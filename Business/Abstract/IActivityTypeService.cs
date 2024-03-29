﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IActivityTypeService
    {
        IDataResult<List<ActivityType>> GetAll();
        IDataResult<ActivityType> GetById(int activityTypeId);
        IResult Add(ActivityType activity);
        IResult Delete(ActivityType activity);
        IResult DeleteAll(Expression<Func<ActivityType, bool>> filter);
        IResult Update(ActivityType activity);
    }
}
