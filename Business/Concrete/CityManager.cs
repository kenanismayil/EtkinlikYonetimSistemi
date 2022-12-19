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
using Entities.DTOs;
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

        [ValidationAspect(typeof(CityValidator))]
        [CacheRemoveAspect("ICityService.Get")]
        public IResult Add(CityRequirements city)
        {
            //Business code
            var cityData = new City()
            {
                CountryId = city.CountryId,
                CityName = city.CityName
            };

            IResult result = BusinessRules.Run(CheckIfCityNameExists(cityData.CityName));
            if (result != null)
            {
                return new ErrorResult(TurkishMessage.CityNameAlreadyExists);
            }

            _cityDal.Add(cityData);
            return new SuccessResult(TurkishMessage.CityAdded);
        }

        [CacheRemoveAspect("ICityService.Get")]
        public IResult Delete(string cityId)
        {
            //Business code
            var cityData = _cityDal.Get(c => c.Id.ToString() == cityId);

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _cityDal.Delete(cityData);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CityDeleted);
        }

        [ValidationAspect(typeof(CityValidator))]
        [CacheRemoveAspect("ICityService.Get")]
        public IResult Update(CityRequirements city)
        {
            //Business code
            var cityData = new City()
            {
                Id = city.CityId,
                CountryId = city.CountryId,
                CityName = city.CityName
            };

            IResult result = BusinessRules.Run(CheckIfCityNameExists(cityData.CityName));
            if (result != null)
            {
                return new ErrorResult(TurkishMessage.CityNameAlreadyExists);
            }

            _cityDal.Update(cityData);
            return new SuccessResult(TurkishMessage.CityUpdated);

        }

        [CacheAspect]
        public IDataResult<List<City>> GetAll()
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<City>>(() =>
            {
                return _cityDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<City>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<City>>(result.Data, TurkishMessage.CitiesListed);
        }

        [CacheAspect]
        public IDataResult<City> GetById(int id)
        {
            //Business code

            //Central Management System
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

        [CacheAspect]

        public IDataResult<List<City>> GetCityByCountryId(int countryId)
        {
            var result = ExceptionHandler.HandleWithReturn<int, List<City>>((int x) =>
            {
                return _cityDal.GetAll(c => c.CountryId == x);
            }, countryId);
            if (!result.Success)
            {
                return new ErrorDataResult<List<City>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<City>>(result.Data, TurkishMessage.SuccessMessage);
        }




        //İş kuralları
        private IResult CheckIfCityNameExists(string cityName)
        {
            //Aynı isimde şehir eklenemez
            var result = _cityDal.GetAll(a => a.CityName == cityName).Any();
            if (result)
            {
                return new ErrorResult(TurkishMessage.CityNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
