using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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
    public class CertificateManager : ICertificateService
    {
        ICertificateDal _certificateDal;

        public CertificateManager(ICertificateDal certificateDal)
        {
            _certificateDal = certificateDal;
        }

        [ValidationAspect(typeof(CertificateValidator))]
        [CacheRemoveAspect("ICertificateService.Get")]
        public IResult Add(Certificate certificate)
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.Add(certificate);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            _certificateDal.Add(certificate);
            return new SuccessResult(TurkishMessage.CertificateAdded);
        }

        [CacheRemoveAspect("ICertificateService.Get")]
        public IResult Delete(Certificate certificate)
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.Delete(certificate);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CertificateDeleted);
        }

        [CacheRemoveAspect("ICertificateService.Get")]
        public IResult DeleteAll(Expression<Func<Certificate, bool>> filter)
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.DeleteAll(filter);
            });

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
        public IDataResult<Certificate> GetById(int id)
        {
            //Business code


            var result = ExceptionHandler.HandleWithReturn<int, Certificate>((x) =>
            {
                return _certificateDal.Get(c => c.Id == x);
            }, id);
            if (!result.Success)
            {
                return new ErrorDataResult<Certificate>(result.Data, TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<Certificate>(result.Data, TurkishMessage.SuccessMessage);
        }

        [ValidationAspect(typeof(CertificateValidator))]
        [CacheRemoveAspect("ICertificateService.Get")]
        public IResult Update(Certificate certificate)
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.Update(certificate);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CertificateUpdate);
        }


        //İş kuralları
    }
}
