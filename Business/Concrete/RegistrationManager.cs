using Business.Abstract;
using Business.Constants.Messages;
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

        public IResult Add(Registration registration)
        {
            //Business code
            var ruleExceptions = BusinessRuleHandler.CheckTheRules(MustUser(registration));
            if (!ruleExceptions.Success)
            {
                return new ErrorResult(ruleExceptions.Message);
            }

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _registrationDal.Add(registration);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.RegistrationAdded);
        }

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

        public IResult DeleteAll(Expression<Func<Registration, bool>> filter)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _registrationDal.DeleteAll(filter);
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

            return new SuccessDataResult<List<Registration>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        public IDataResult<Registration> GetById(int id)
        {
            //Business code
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


        public IResult Update(Registration registration)
        {
            //Business code
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorResult(TurkishMessage.MaintenanceTime);
            }

            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _registrationDal.Update(registration);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.RegistrationUpdated);
        }


        private IResult MustUser(Registration registration)
        {
            var registers = _registrationDal.Get(r => r.UserId == registration.ActivityId);
            if (registers != null)
            {
                return new ErrorResult("Bir kullanici ayni aktiviteye tekrar kayit yapamaz");
            }

            return new SuccessResult(TurkishMessage.SuccessMessage);
        }


    }
}
