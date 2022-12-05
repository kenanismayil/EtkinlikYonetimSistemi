using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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

        [ValidationAspect(typeof(CountryValidator))]
        [CacheRemoveAspect("ICountryService.Get")]
        public IResult Add(Country country)
        {
            //Business code

            IResult result = BusinessRules.Run(CheckIfCountryNameExists(country.CountryName));
            if (result != null)
            {
                return new ErrorResult(TurkishMessage.CountryNameAlreadyExists);
            }

            _countryDal.Add(country);
            return new SuccessResult(TurkishMessage.CountryAdded);


            //Central Management System
            //var result = ExceptionHandler.HandleWithNoReturn(() =>
            //{
            //    _countryDal.Add(country);
            //});
            //if (!result)
            //{
            //    return new ErrorResult(TurkishMessage.ErrorMessage);
            //}

            //return new SuccessResult(TurkishMessage.CountryAdded);
        }

        [CacheRemoveAspect("ICountryService.Get")]
        public IResult Delete(Country country)
        {
            //Business code

            //Central Management System
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

        //public IResult DeleteAll(Expression<Func<Country, bool>> filter)
        //{
        //    //Business code
        //    var result = ExceptionHandler.HandleWithNoReturn(() =>
        //    {
        //        _countryDal.DeleteAll(filter);
        //    });
        //    if (!result)
        //    {
        //        return new ErrorResult(TurkishMessage.ErrorMessage);
        //    }

        //    return new SuccessResult(TurkishMessage.CountryDeleted);
        //}

        [CacheAspect]
        public IDataResult<List<Country>> GetAll()
        {
            //Business code

            
            //Central Management System
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

        [CacheAspect]
        public IDataResult<Country> GetById(int id)
        {
            //Business code

            //Central Management System
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

        [ValidationAspect(typeof(CountryValidator))]
        [CacheRemoveAspect("ICountryService.Get")]
        public IResult Update(Country country)
        {
            //Business code
            IResult result = BusinessRules.Run(CheckIfCountryNameExists(country.CountryName));
            if (result != null)
            {
                return new ErrorResult(TurkishMessage.CountryNameAlreadyExists);
            }

            _countryDal.Add(country);
            return new SuccessResult(TurkishMessage.CountryAdded);

            //Central Management System
            //var result = ExceptionHandler.HandleWithNoReturn(() =>
            //{
            //    _countryDal.Update(country);
            //});
            //if (!result)
            //{
            //    return new ErrorResult(TurkishMessage.ErrorMessage);
            //}

            //return new SuccessResult(TurkishMessage.CountryUpdated);
        }




        //İş kuralları
        private IResult CheckIfCountryNameExists(string countryName)
        {
            //Aynı isimde şehir eklenemez
            var result = _countryDal.GetAll(a => a.CountryName == countryName).Any();
            if (result)
            {
                return new ErrorResult(TurkishMessage.CountryNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
