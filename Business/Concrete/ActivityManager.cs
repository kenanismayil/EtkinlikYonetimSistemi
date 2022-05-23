using Business.Abstract;
using Business.Constants.Messages;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ActivityManager : IActivityService
    {
        IActivityDal _activityDal;
        //IMessage _message;
        public ActivityManager(IActivityDal activityDal/*IMessage message*/)
        {
            _activityDal = activityDal;
            //_message = message;
        }


        public IResult Add(Activity activity)
        {
            //Business code


            return new SuccessResult(Message.ActivityAdded);
        }

        public IResult Delete(Activity activity)
        {
            //Business code

            _activityDal.Delete(activity);
            return new SuccessResult(Message.ActivityDeleted);
        }

        public IResult DeleteAll(Expression<Func<Activity, bool>> filter)
        {
            //Business code

            _activityDal.DeleteAll(filter);
            return new SuccessResult(Message.ActivityDeleted);
        }

        //Validation
        public IDataResult<List<ActivityDetailDto>> GetActivityDetails()
        {
            //business code

            var data = _activityDal.GetActivityDetails();
            return new SuccessDataResult<List<ActivityDetailDto>>(data);
        }

        public IDataResult<List<Activity>> GetAll()
        {
            //Business code

            var data = _activityDal.GetAll();
            return new SuccessDataResult<List<Activity>>(data, Message.ActivitiesListed);
        }

        public IDataResult<Activity> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Activity>(Message.MaintenanceTime);
            }

            var data = _activityDal.Get(c => c.Id == id);
            return new SuccessDataResult<Activity>(data);
        }


        public IResult Update(Activity activity)
        {
            //Business code

            _activityDal.Update(activity);
            return new SuccessResult(Message.ActivityUpdated);
        }
    }
}
