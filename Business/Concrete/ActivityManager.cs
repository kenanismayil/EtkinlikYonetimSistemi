using Business.Abstract;
using Business.Constants.Messages;
using Core.BusinessRuleHandle;
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
        IModeratorDal _moderatorDal;
        public ActivityManager(IActivityDal activityDal, IModeratorDal moderatorDal)
        {
            _activityDal = activityDal;
            _moderatorDal = moderatorDal;
        }
  
        //Validation
        public IResult Add(Activity activity)
        {
            //Business codes
            //var ruleExceptions = BusinessRuleHandler.CheckTheRules(MustModerator(activity));
            //if (!ruleExceptions.Success)
            //{
            //    return new ErrorResult(ruleExceptions.Message);
            //}

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityDal.Add(activity);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityAdded);
        }

        //Validation
        public IResult Delete(Activity activity)
        {
            //Business codes
            //var ruleExceptions = BusinessRuleHandler.CheckTheRules(MustModerator(activity));
            //if (!ruleExceptions.Success)
            //{
            //    return new ErrorResult(ruleExceptions.Message);
            //}

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityDal.Delete(activity);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityDeleted);
        }

        //Validation
        public IResult DeleteAll(Expression<Func<Activity, bool>> filter)
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityDal.DeleteAll(filter);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityDeleted);
        }

        //Validation
        public IDataResult<List<ActivityDetailDto>> GetActivityDetails()
        {
            //business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<ActivityDetailDto>>(() =>
            {
                return _activityDal.GetActivityDetails();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<ActivityDetailDto>>(TurkishMessage.ErrorMessage);
            }
            
            return new SuccessDataResult<List<ActivityDetailDto>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        //Validation
        public IDataResult<List<Activity>> GetAll()
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Activity>>(() =>
            {
                return _activityDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Activity>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<Activity>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        //Validation
        public IDataResult<Activity> GetById(int id)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, Activity>((x) =>
            {
                return _activityDal.Get(a => a.Id == x);
            }, id);
            if (!result.Success)
            {
                return new ErrorDataResult<Activity>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<Activity>(result.Data, TurkishMessage.SuccessMessage);
        }

        //Validation
        public IResult Update(Activity activity)
        {
            //Business code
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Activity>(TurkishMessage.MaintenanceTime);
            }

            
            //var ruleExceptions = BusinessRuleHandler.CheckTheRules(MustModerator(activity));
            //if (!ruleExceptions.Success)
            //{
            //    return new ErrorResult(ruleExceptions.Message);
            //}

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityDal.Update(activity);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityUpdated);
        }

        //private IResult MustModerator(Activity activity)
        //{
        //    var moderator = _moderatorDal.Get(x => x.Id == activity.ModeratorId);
        //    if(moderator == null)
        //    {
        //        return new ErrorResult("Aktivite üzerinde sadece moderator işlem yapabilir");
        //    }
        //    return new SuccessResult(TurkishMessage.SuccessMessage);
        //}
    }
}
