using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.BusinessRuleHandle;
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
    public class RegistrationManager : IRegistrationService
    {
        IRegistrationDal _registrationDal;


        public RegistrationManager(IRegistrationDal registrationDal)
        {
            _registrationDal = registrationDal;

        }


        [ValidationAspect(typeof(RegistrationValidator))]
        [CacheRemoveAspect("IRegistrationService.Get")]
        public IResult Add(Registration registration)
        {
            //Business code
            IResult result = BusinessRuleHandler.
                CheckTheRules(CheckIfUserLimitsExceeded(registration.User.Id, registration.Activity.Id));
            if (result != null)
            {
                return result;
            }

            //Central Management System
            //var result = ExceptionHandler.HandleWithNoReturn(() =>
            //{
            //    _registrationDal.Add(registration);
            //});
            //if (!result)
            //{
            //    return new ErrorResult(TurkishMessage.ErrorMessage);
            //}

            _registrationDal.Add(registration);
            return new SuccessResult(TurkishMessage.RegistrationAdded);
        }

        [CacheRemoveAspect("IRegistrationService.Get")]
        public IResult Delete(Registration registration)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _registrationDal.Delete(registration);
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
        public IResult Update(Registration registration)
        {
            //Business code
            IResult result = BusinessRuleHandler.
                CheckTheRules(CheckIfUserLimitsExceeded(registration.User.Id, registration.Activity.Id));
            if (result != null)
            {
                return result;
            }
            _registrationDal.Update(registration);
            return new SuccessResult(TurkishMessage.RegistrationUpdated);


            //Central Management System
            //var result = ExceptionHandler.HandleWithNoReturn(() =>
            //{
            //    _registrationDal.Update(registration);
            //});
            //if (!result)
            //{
            //    return new ErrorResult(TurkishMessage.ErrorMessage);
            //}

            //return new SuccessResult(TurkishMessage.RegistrationUpdated);
        }


        //İŞ KURALLARI
        private IResult CheckIfUserLimitsExceeded(int userId, int activityId)
        {
            var register = _registrationDal.Get(r => r.UserId == userId);
            if (register.ActivityId == activityId)
            {
                return new ErrorResult("Bir kullanici ayni aktiviteye tekrar kayit yapamaz");
            }

            return new SuccessResult(TurkishMessage.SuccessMessage);
        }





    }
}
