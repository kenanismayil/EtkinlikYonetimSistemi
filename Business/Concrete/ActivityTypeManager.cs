using Business.Abstract;
using Business.Constants.Messages;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityTypeDal.Add(activityType);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityTypeAdded);
        }

        //Validation
        public IResult Delete(ActivityType activityType)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityTypeDal.Delete(activityType);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityDeleted);
        }

        //Validation
        public IResult DeleteAll(Expression<Func<ActivityType, bool>> filter)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityTypeDal.DeleteAll(filter);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityTypeDeleted);
        }

        //Validation
        public IDataResult<List<ActivityType>> GetAll()
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<ActivityType>>(() =>
            {
                return _activityTypeDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<ActivityType>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<ActivityType>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        //Validation
        public IDataResult<ActivityType> GetById(int id)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, ActivityType>((x) =>
            {
                return _activityTypeDal.Get(a => a.Id == x);
            }, id);
            if (!result.Success)
            {
                return new ErrorDataResult<ActivityType>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<ActivityType>(result.Data, TurkishMessage.SuccessMessage);
        }

        //Validation
        public IResult Update(ActivityType activityType)
        {
            //Business code
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorResult(TurkishMessage.MaintenanceTime);
            }

            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityTypeDal.Update(activityType);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityUpdated);
        }
    }
}
