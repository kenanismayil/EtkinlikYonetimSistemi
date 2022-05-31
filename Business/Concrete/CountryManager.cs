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
    public class CountryManager : ICountryService 
    {
        ICountryDal _countryDal;
        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }


        public IResult Add(Country country)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _countryDal.Add(country);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CountryAdded);
        }

        public IResult Delete(Country country)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _countryDal.Delete(country);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CountryDeleted);
        }

        public IResult DeleteAll(Expression<Func<Country, bool>> filter)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _countryDal.DeleteAll(filter);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CountryDeleted);
        }

        public IDataResult<List<Country>> GetAll()
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Country>>(() =>
            {
                return _countryDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Country>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<Country>>(result.Data, TurkishMessage.CountriesListed);
        }

        public IDataResult<Country> GetById(int id)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, Country>((int x) =>
            {
                return _countryDal.Get(c => c.Id == x);
            }, id);
            if (!result.Success)
            {
                return new ErrorDataResult<Country>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<Country>(result.Data, TurkishMessage.SuccessMessage);
        }


        public IResult Update(Country country)
        {
            //Business code
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorResult(TurkishMessage.MaintenanceTime);
            }

            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _countryDal.Update(country);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CountryUpdated);
        }
    }
}
