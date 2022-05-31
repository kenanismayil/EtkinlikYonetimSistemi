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
    public class CityManager : ICityService
    {
        ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }


        public IResult Add(City city)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _cityDal.Add(city);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CityAdded);
        }

        public IResult Delete(City city)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _cityDal.Delete(city);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CityDeleted);
        }

        public IResult DeleteAll(Expression<Func<City, bool>> filter)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _cityDal.DeleteAll(filter);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CityDeleted);
        }

        public IDataResult<List<City>> GetAll()
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<City>>(() =>
            {
                return _cityDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<City>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<City>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        public IDataResult<City> GetById(int id)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, City>((int x) =>
            {
                return _cityDal.Get(c => c.Id == x);
            }, id);
            if (!result.Success)
            {
                return new ErrorDataResult<City>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<City>(result.Data, TurkishMessage.SuccessMessage);
        }


        public IResult Update(City city)
        {
            //Business code
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorResult(TurkishMessage.MaintenanceTime);
            }

            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _cityDal.Update(city);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CityUpdated);
        }
    }
}
