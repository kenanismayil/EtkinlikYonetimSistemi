using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.CCS;
using Business.Constants.Messages;
using Business.Helper;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.BusinessRuleHandle;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        //Bir entity manager, kendi Dal'ı hariç başka bir Dal'ı enjekte edemez!
        IActivityDal _activityDal;
        IActivityTypeService _activityTypeService;  //aktivite tipi tablosunu ilgilendiren bir kural olduğu için bu servisi enjekte deriz, Dal'ı değil!
        IUserService _userService;
        IAuthHelper _authHelper;
        //IRegistrationService _registrationService;
        public ActivityManager(IActivityDal activityDal, IActivityTypeService activityTypeService, IUserService userService, IAuthHelper authHelper)
        {
            _activityDal = activityDal;
            _activityTypeService = activityTypeService;
            _userService = userService;
            _authHelper = authHelper;
            //_registrationService = registrationService;
        }

        //Validation -> Add metotunu ActivityValidator'daki kurallara göre doğrulanması
        //Claim -> admin, activity.add,super_admin.  activity.add -> operasyon bazlı yapılarda kullanılır. 
        [ValidationAspect(typeof(ActivityValidator))]
        [CacheRemoveAspect("IActivityService.Get")]
        public IResult Add(ActivityCreatingByAdmin activity, string token)
        {
            var currentUserId = _authHelper.GetCurrentUser(token).Data.Id;
            //Business codes -> Polymorphism yaptım.
            var activityData = new Activity()
            {
                Id = activity.Id,
                UserId = currentUserId,
                ActivityTypeId = activity.ActivityTypeId,
                Title = activity.Title,
                Description = activity.Description,
                Image = activity.Image,
                LocationId = activity.LocationId,
                Participiant = activity.Participiant,
                ActivityDate = activity.ActivityDate,
                CityId = activity.CityId,
                CountryId = activity.CountryId,
                CreatedTime = DateTime.Now
            };

            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityDal.Add(activityData);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }
            
            return new SuccessResult(TurkishMessage.ActivityAdded);
        }


        [CacheRemoveAspect("IActivityService.Get")]
        public IResult Delete(string activityId)
        {
            //Business codes
            var activity = _activityDal.Get(a => a.Id.ToString() == activityId);

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
        [CacheRemoveAspect("IActivityService.Get")]
        public IResult Update(ActivityCreatingByAdmin activity, string token)
        {
            //Business code
            var currentUserId = _authHelper.GetCurrentUser(token).Data.Id;
            var activityData = new Activity()
            {
                UserId = currentUserId,
                ActivityTypeId = activity.ActivityTypeId,
                Title = activity.Title,
                Description = activity.Description,
                Image = activity.Image,
                LocationId = activity.LocationId,
                Participiant = activity.Participiant,
                ActivityDate = activity.ActivityDate,
                CityId = activity.CityId,
                CountryId = activity.CountryId,
                CreatedTime = activity.CreatedTime,
                Id = activity.Id
            };

            var result = ExceptionHandler.HandleWithNoReturn(() => {
                _activityDal.Update(activityData);
            });

            if(!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityUpdated);
        }

        [CacheAspect]
        public IResult UpdateParticipiant(int activityId, int participiant)
        {
            //Business code
            var activity = _activityDal.Get(a => a.Id == activityId);
            activity.Participiant = participiant;

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

        [CacheRemoveAspect("IActivityService.Get")]
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

       //Cachlemek istediğimiz bir datayı bellekte Key,Value olarak tutuyoruz.
        [CacheAspect]
        public IDataResult<List<ActivityForView>> GetAll()
        {
            //Business code


            //Cetnral Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<ActivityForView>>(() =>
            {
                return _activityDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<ActivityForView>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<ActivityForView>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        [CacheAspect]
        public IDataResult<ActivityForView> GetById(int activityId)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, ActivityForView>((x) =>
            {
                var activity = _activityDal.GetById(x);
                
                return activity;
            }, activityId);
            if (!result.Success)
            {
                return new ErrorDataResult<ActivityForView>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<ActivityForView>(result.Data, TurkishMessage.SuccessMessage);
        }

        //public IDataResult<Activity> GetCurrentAttendiesCount(int activityId)
        //{
        //    //Activity tablosuna maksimum katılımcı sayısı da eklenmesi gerekiyor.

        //    var activityData = _activityDal.GetAll(a => a.Id == activityId).Count;



        //    var registeredToActivity = _registrationService.GetById(activityId);

        //    int currentUserCount, maxUserCount;
        //    currentUserCount = registeredToActivity.Data.User.Id;

        //    if (activityData.)
        //    {

        //    }


        //    return new SuccessDataResult<Activity>(TurkishMessage.SuccessMessage);
        //}
    }
}
