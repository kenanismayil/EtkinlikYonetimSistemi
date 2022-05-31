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
    public class CertificateManager : ICertificateService
    {
        ICertificateDal _certificateDal;

        public CertificateManager(ICertificateDal certificateDal)
        {
            _certificateDal = certificateDal;
        }

        //Validation
        public IResult Add(Certificate certificate)
        {
            //Business code
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

        //Validation
        public IResult Delete(Certificate certificate)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.Delete(certificate);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityDeleted);
        }

        //Validation
        public IResult DeleteAll(Expression<Func<Certificate, bool>> filter)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.DeleteAll(filter);
            });

            return new SuccessResult(TurkishMessage.ActivityDeleted);
        }

        //Validation
        public IDataResult<List<Certificate>> GetAll()
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Certificate>>(() =>
            {
                return _certificateDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Certificate>>(TurkishMessage.ErrorMessage);
            }


            return new SuccessDataResult<List<Certificate>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        //Validation
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

        //Validation
        public IResult Update(Certificate certificate)
        {
            //Business code
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorResult(TurkishMessage.MaintenanceTime);
            }

            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _certificateDal.Update(certificate);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityUpdated);
        }


    }
}
