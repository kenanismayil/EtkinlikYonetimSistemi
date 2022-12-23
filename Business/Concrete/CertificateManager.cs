using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.Constants.Messages;
using Business.Helper;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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
    public class CertificateManager : ICertificateService
    {
        ICertificateDal _certificateDal;
        //IUserService _userservice;
        IRegistrationService _registrationService;
        IAuthHelper _authHelper;

        public CertificateManager(ICertificateDal certificateDal, IRegistrationService registrationService, IAuthHelper authHelper)
        {
            _certificateDal = certificateDal;
            //_userservice = userService;
            _authHelper = authHelper;
            _registrationService = registrationService;
        }

        [ValidationAspect(typeof(CertificateValidator))]
        [CacheRemoveAspect("ICertificateService.Get")]
        public IResult Add(CertificateForView certificate, string pnrNo)
        {
            //Business code
            var regData = _registrationService.GetRegisterInfoByUserAndActivityId(certificate.UserId, certificate.ActivityId);

            var certificateForView = new Certificate()
            {
                UserId = regData.Data.UserId,
                ActivityId = regData.Data.ActivityId,
                GivenDate = certificate.GivenDate,
                ExpiryDate = certificate.ExpiryDate
            };

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                if (regData.Data.isUserOnEventPlace == true)
                {
                    _certificateDal.Add(certificateForView);
                }
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CertificateAdded);
        }

        [ValidationAspect(typeof(CertificateValidator))]
        [CacheRemoveAspect("ICertificateService.Get")]
        public IResult Update(CertificateForView certificate)
        {
            //Business code
            var regData = _registrationService.GetRegisterInfoByUserAndActivityId(certificate.UserId, certificate.ActivityId);

            var certificateForView = new Certificate()
            {
                Id = certificate.Id,
                UserId = regData.Data.UserId,
                ActivityId = regData.Data.ActivityId,
                GivenDate = certificate.GivenDate,
                ExpiryDate = certificate.ExpiryDate
            };

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.Update(certificateForView);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CertificateUpdate);
        }

        [CacheRemoveAspect("ICertificateService.Get")]
        public IResult Delete(int certificateId)
        {
            //Business code
            var certificateData = _certificateDal.Get(c => c.Id == certificateId);

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.Delete(certificateData);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CertificateDeleted);
        }


        [CacheAspect]
        public IDataResult<List<Certificate>> GetAll()
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Certificate>>(() =>
            {
                return _certificateDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Certificate>>(TurkishMessage.ErrorMessage);
            }


            return new SuccessDataResult<List<Certificate>>(result.Data, TurkishMessage.CertificateListed);
        }

        [CacheAspect]
        public IDataResult<UserCertificateInfo> GetByCertificateId(int certificateId)
        {
            //Business code


            var result = ExceptionHandler.HandleWithReturn<int, UserCertificateInfo>((x) =>
            {
                return _certificateDal.GetByCertificateId(certificateId);
            }, certificateId);
            if (!result.Success)
            {
                return new ErrorDataResult<UserCertificateInfo>(result.Data, TurkishMessage.ErrorMessage);
            }

            //var result = _certificateDal.GetByCertificateId(certificateId);
            return new SuccessDataResult<UserCertificateInfo>(result.Data, TurkishMessage.SuccessMessage);
        }

        public IDataResult<List<UserCertificateInfo>> GetCertificatesForUser(string token)
        {
            //Business codes
            var currentUserId = _authHelper.GetCurrentUser(token).Data.Id;
            var result = ExceptionHandler.HandleWithReturn<int, List<UserCertificateInfo>>((int x) =>
            {
                return _certificateDal.GetCertificatesForUser(x);
            }, currentUserId);

            if (!result.Success)
            {
                return new ErrorDataResult<List<UserCertificateInfo>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<UserCertificateInfo>>(result.Data, TurkishMessage.SuccessMessage);
        }

        //public IDataResult<UserCertificateInfo> GetCertificates(string token)
        //{
        //    var currentUser = _authHelper.GetCurrentUser(token).Data;


        //    var result = _certificateDal.GetCertificates(activityId);

        //    return new SuccessDataResult<UserCertificateInfo>(result, TurkishMessage.SuccessMessage);
        //}





        //İş kuralları

    }
}
