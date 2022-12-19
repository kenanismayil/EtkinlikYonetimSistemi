using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.Constants.Messages;
using Business.Helper;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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
    public class RegistrationManager : IRegistrationService
    {
        IRegistrationDal _registrationDal;
        IAuthHelper _authHelper;

        //IActivityService _activityService;


        public RegistrationManager(IRegistrationDal registrationDal, IAuthHelper authHelper)
        {
            _registrationDal = registrationDal;
            _authHelper = authHelper;
            //_activityService = activityService;
        }


        [ValidationAspect(typeof(RegistrationValidator))]
        [CacheRemoveAspect("IRegistrationService.Get")]
        public IResult Add(string token, int  activityId)
        {
            //Business code
            //var activityData = _activityService.GetById(activityId);

            var currentUser = _authHelper.GetCurrentUser(token).Data;
            Registration registerForActivity = new Registration()
            {
                UserId = currentUser.Id,
                ActivityId = activityId,
                Date = DateTime.Now
            };

            var register = _registrationDal.GetAll(r => r.UserId == registerForActivity.UserId && r.ActivityId == activityId);
            if (register.Count >= 1)
            {
                return new ErrorResult("Bir kullanici ayni aktiviteye tekrar kayit yapamaz");
            }

            //if (activityData.Data.Participiant == 0)
            //{
            //    return new ErrorResult("Etkinliğe katılımcı sayısı aşıldığından katılamazsınız");
            //}



            //activityData.Data.Participiant--;
            _registrationDal.Add(registerForActivity);


            //var activity = new ActivityCreatingByAdmin()
            //{
            //    UserId 
            //};

            //_activityService.Update(activityData);

            return new SuccessResult(TurkishMessage.RegistrationAdded);
        }

        [CacheRemoveAspect("IRegistrationService.Get")]
        public IResult Delete(RegisterForActivity registration)
        {
            //Business code
            var register = _registrationDal.Get(r => r.UserId == registration.UserId && r.ActivityId == registration.ActivityId);

            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _registrationDal.Delete(register);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.RegistrationDeleted);
        }


        public IDataResult<List<Registration>> GetAll()
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Registration>>(() =>
            {
                return _registrationDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Registration>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<Registration>>(result.Data, TurkishMessage.RegistrationListed);
        }

        public IDataResult<List<Registration>> GetAllForFilter(Expression<Func<Registration, bool>> filter)
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _registrationDal.GetAll(filter);
            });
            if (!result)
            {
                return new ErrorDataResult<List<Registration>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<Registration>>(TurkishMessage.ActivityDeleted);
        }

        public IDataResult<Registration> GetById(int id)
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithReturn<int, Registration>((int x) =>
            {
                return _registrationDal.Get(r => r.Id == x);
            }, id);
            if (!result.Success)
            {
                return new ErrorDataResult<Registration>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<Registration>(result.Data, TurkishMessage.SuccessMessage);
        }


        [ValidationAspect(typeof(RegistrationValidator))]
        [CacheRemoveAspect("IRegistrationService.Get")]
        public IResult Update(RegisterForActivity registration)
        {
            //Business code
            //IResult result = BusinessRuleHandler.
            //    CheckTheRules(CheckIfUserLimitsExceeded(registration.User.Id, registration.Activity.Id));
            //if (result != null)
            //{
            //    return result;
            //}
            var register = _registrationDal.GetAll(r => r.UserId == registration.UserId && r.ActivityId == registration.ActivityId).Count;
            if (register >= 1)
            {
                return new ErrorResult("Bir kullanici ayni aktiviteye tekrar kayit yapamaz");
            }

            var result = new Registration()
            {
                UserId = registration.UserId,
                ActivityId = registration.ActivityId
            };

            result.Date = DateTime.Now;
            _registrationDal.Update(result);
            return new SuccessResult(TurkishMessage.RegistrationUpdated);

        }


        //İŞ KURALLARI
        //private IResult CheckIfUserLimitsExceeded(int userId, int activityId)
        //{
        //    var register = _registrationDal.Get(r => r.UserId == userId && r.ActivityId == activityId);
        //    if (register != null)
        //    {
        //        return new ErrorResult("Bir kullanici ayni aktiviteye tekrar kayit yapamaz");
        //    }

        //    return new SuccessResult(TurkishMessage.SuccessMessage);
        //}





    }
}
