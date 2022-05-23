using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ActivityTypeManager : IActivityTypeService
    {
        IActivityTypeDal _activityTypeDal;
        public ActivityTypeManager(IActivityTypeDal activityTypeDal)
        {
            _activityTypeDal = activityTypeDal;
        }

        //Validation
        public IResult Add(ActivityType activityType)
        {
            //Business code

            _activityTypeDal.Add(activityType);
            return new SuccessResult(Message.ActivityTypeAdded);
        }

        //Validation
        public IResult Delete(ActivityType activityType)
        {
            //Business code

            _activityTypeDal.Delete(activityType);
            return new SuccessResult(Message.ActivityDeleted);
        }

        //Validation
        public IResult DeleteAll(Expression<Func<ActivityType, bool>> filter)
        {
            //Business code

            _activityTypeDal.DeleteAll(filter);
            return new SuccessResult(Message.ActivityTypeDeleted);
        }

        //Validation
        public IDataResult<List<ActivityType>> GetAll()
        {
            //Business code

            var data = _activityTypeDal.GetAll();
            return new SuccessDataResult<List<ActivityType>>(data, Message.ActivitiesListed);
        }

        //Validation
        public IDataResult<ActivityType> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<ActivityType>(Message.MaintenanceTime);
            }

            var data = _activityTypeDal.Get(c => c.Id == id);
            return new SuccessDataResult<ActivityType>(data);
        }

        //Validation
        public IResult Update(ActivityType activityType)
        {
            //Business code

            _activityTypeDal.Update(activityType);
            return new SuccessResult(Message.ActivityUpdated);
        }
    }
}
