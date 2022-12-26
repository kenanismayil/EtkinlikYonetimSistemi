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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RegistrationManager : IRegistrationService
    {
        IRegistrationDal _registrationDal;
        IAuthHelper _authHelper;
        IActivityService _activityService;
        ICertificateService _certificateService;


        public RegistrationManager(IRegistrationDal registrationDal, IAuthHelper authHelper, IActivityService activityService, ICertificateService certificateService)
        {
            _registrationDal = registrationDal;
            _authHelper = authHelper;
            _activityService = activityService;
            _certificateService = certificateService;
        }


        [ValidationAspect(typeof(RegistrationValidator))]
        [CacheRemoveAspect("IRegistrationService.Get")]
        public IResult Add(string token, int activityId)
        {
            //Business code
            var currentUser = _authHelper.GetCurrentUser(token).Data;

            Registration registerForActivity = new Registration()
            {
                UserId = currentUser.Id,
                ActivityId = activityId,
                Date = DateTime.Now,
                PnrNo = RandomNumberGenerator.Create().GetHashCode().ToString()
            };

            var register = _registrationDal.GetAll(r => r.UserId == registerForActivity.UserId && r.ActivityId == activityId);
            if (register.Count >= 1)
            {
                return new ErrorResult("Bir kullanici ayni aktiviteye tekrar kayit yapamaz");
            }

            var activity = _activityService.GetById(activityId).Data;
            if (activity.Participiant == 0)
            {
                return new ErrorResult("Etkinliğe katılımcı sayısı aşıldığından katılamazsınız");
            }

            var newParticipiant = activity.Participiant - 1;
            _activityService.UpdateParticipiant(activityId, newParticipiant);

            _registrationDal.Add(registerForActivity);
            return new SuccessResult(TurkishMessage.RegistrationAdded);
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

        [CacheRemoveAspect("IRegistrationService.Get")]
        public IResult Delete(RegisterForActivity registration)
        {
            //Business code
            var register = _registrationDal.Get(r => r.UserId == registration.UserId && r.ActivityId == registration.ActivityId);


            if (!register.isUserOnEventPlace)
            {
                _registrationDal.Delete(register);
                var activity = _activityService.GetById(registration.ActivityId).Data;
                var newParticipiant = activity.Participiant + 1;
                _activityService.UpdateParticipiant(registration.ActivityId, newParticipiant);
                return new SuccessResult(TurkishMessage.RegistrationDeleted);
            }
            return new ErrorResult("Etkinlik alanına giriş yaptığınız için kaydınızı silemezsiniz.");
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

        [CacheAspect]
        public IDataResult<List<Registration>> GetRegistersInfoByUserId(string token)
        {
            //Business code
            var currentUserId = _authHelper.GetCurrentUser(token).Data.Id;
            var result = _registrationDal.GetAll(r => r.UserId == currentUserId);
            return new SuccessDataResult<List<Registration>>(result, TurkishMessage.SuccessMessage);
        }
        [CacheAspect]
        public IDataResult<List<RegistrationForTickets>> GetRegistersInfoByUserIdForTickets(string token)
        {
            //Business code
            var currentUserId = _authHelper.GetCurrentUser(token).Data.Id;
            var result = _registrationDal.GetAllForTickets(currentUserId);
            return new SuccessDataResult<List<RegistrationForTickets>>(result, TurkishMessage.SuccessMessage);
        }

        [CacheAspect]
        public IDataResult<Registration> GetRegisterInfoByUserAndActivityId(int userId, int activityId)
        {
            //Business code

            var result = _registrationDal.Get(r => r.UserId == userId && r.ActivityId == activityId);
            return new SuccessDataResult<Registration>(result, TurkishMessage.SuccessMessage);
        }

        public IDataResult<UserRegisterInfo> GetRegisterInfo(int activityId, string token)
        {
            var currentUser = _authHelper.GetCurrentUser(token).Data;
            var isRegister = _registrationDal.Get(r => r.UserId == currentUser.Id && r.ActivityId == activityId);

            if (isRegister != null)
            {
                return new SuccessDataResult<UserRegisterInfo>(new UserRegisterInfo()
                {
                    IsRegistered = true,
                    UserId = currentUser.Id,
                    ActivityId = activityId,
                });
            }

            return new SuccessDataResult<UserRegisterInfo>(new UserRegisterInfo()
            {
                IsRegistered = false,
                UserId = currentUser.Id,
                ActivityId = activityId,
            });
        }

        public IDataResult<List<UserRegisteredEventsInfo>> GetRegisteredEvents(string token)
        {
            var currentUserId = _authHelper.GetCurrentUser(token).Data.Id;
            var result = ExceptionHandler.HandleWithReturn<int, List<UserRegisteredEventsInfo>>((int x) =>
            {


                return _registrationDal.GetRegisteredEvents(x);
            }, currentUserId);

            if (!result.Success)
            {
                return new ErrorDataResult<List<UserRegisteredEventsInfo>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<UserRegisteredEventsInfo>>(result.Data, TurkishMessage.SuccessMessage);
        }

        public IDataResult<UserInfoForBarcodeReaderPerson> GetUserByPnrNo(string pnrNo)
        {
            var result = ExceptionHandler.HandleWithReturn<string, UserInfoForBarcodeReaderPerson>((string x) =>
            {
                return _registrationDal.GetUserByPnrNo(x);
            }, pnrNo);

            if (!result.Success || result.Data == null)
            {
                return new ErrorDataResult<UserInfoForBarcodeReaderPerson>(TurkishMessage.ErrorMessage);
            }

            if (result.Data.isUserOnEventPlace)
            {
                return new ErrorDataResult<UserInfoForBarcodeReaderPerson>(TurkishMessage.UserAlreadyOnEventArea);
            }

            return new SuccessDataResult<UserInfoForBarcodeReaderPerson>(result.Data, TurkishMessage.SuccessMessage);
        }
        public IDataResult<UserInfoForBarcodeReaderPerson> UpdateUserStatusOnEventArea(string pnrNo)
        {
            var result = ExceptionHandler.HandleWithReturn<string, UserInfoForBarcodeReaderPerson>((string x) =>
            {
                var info = _registrationDal.GetUserByPnrNo(x);
                info.isUserOnEventPlace = true;

                return info;
            }, pnrNo);

            if (!result.Success || result.Data == null)
            {
                return new ErrorDataResult<UserInfoForBarcodeReaderPerson>(TurkishMessage.ErrorMessage);
            }

            var updateRegistration = _registrationDal.Get(r => r.PnrNo == pnrNo);
            if (updateRegistration.isUserOnEventPlace)
            {
                return new ErrorDataResult<UserInfoForBarcodeReaderPerson>(TurkishMessage.UserAlreadyOnEventArea);
            }

            updateRegistration.isUserOnEventPlace = true;

            _registrationDal.Update(updateRegistration);


            var registrationActivityId = Convert.ToInt32(updateRegistration.ActivityId);
            var registrationUserId = Convert.ToInt32(updateRegistration.UserId);

            var certificate = new CertificateForView()
            {
                ActivityId = registrationActivityId,
                UserId = registrationUserId,
                GivenDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddYears(1),
            };

            var isCertificateExists = _certificateService.Get(registrationUserId, registrationActivityId).Data;
            //Central Management System
            var certificateResult = ExceptionHandler.HandleWithNoReturn(() =>
            {
                if (isCertificateExists == null)
                {
                    _certificateService.Add(certificate);
                }
            });

            return new SuccessDataResult<UserInfoForBarcodeReaderPerson>(result.Data, TurkishMessage.SuccessMessage);
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
