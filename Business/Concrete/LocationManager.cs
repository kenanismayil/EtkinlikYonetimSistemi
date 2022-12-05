using Business.Abstract;
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
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LocationManager : ILocationService
    {
        ILocationDal _locationDal;
        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        [ValidationAspect(typeof(LocationValidator))]
        [CacheRemoveAspect("ILocation.Get")]
        public IResult Add(Location location)
        {
            //Business code
            IResult result = BusinessRules.Run(CheckIfLocationNameExists(location.Name));
            if (result != null)
            {
                return result;
            }
            _locationDal.Update(location);
            return new SuccessResult(TurkishMessage.LocationUpdated);


            //Central Management System
            //var result = ExceptionHandler.HandleWithNoReturn(() =>
            //{
            //    _locationDal.Add(location);
            //});
            //if (!result)
            //{
            //    return new ErrorResult(TurkishMessage.ErrorMessage);
            //}

            //return new SuccessResult(TurkishMessage.LocationAdded);
        }

        [ValidationAspect(typeof(LocationValidator))]
        [CacheRemoveAspect("ILocation.Get")]
        public IResult Delete(Location location)
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _locationDal.Delete(location);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.LocationDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Location>> GetAll()
        {
            //Business code



            //Central Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Location>>(() =>
            {
                return _locationDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Location>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<Location>>(result.Data, TurkishMessage.CommentsListed);
        }

        [CacheAspect]
        public IDataResult<Location> GetById(int locationId)
        {
            //Business code



            //Central Management System
            var result = ExceptionHandler.HandleWithReturn<int, Location>((int x) =>
            {
                return _locationDal.Get(c => c.Id == x);
            }, locationId);
            if (!result.Success)
            {
                return new ErrorDataResult<Location>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<Location>(result.Data, TurkishMessage.SuccessMessage);
        }


        [ValidationAspect(typeof(LocationValidator))]
        [CacheRemoveAspect("ILocation.Get")]
        public IResult Update(Location location)
        {
            //Business code
            IResult result = BusinessRules.Run(CheckIfLocationNameExists(location.Name));
            if (result != null)
            {
                return result;
            }
            _locationDal.Update(location);
            return new SuccessResult(TurkishMessage.LocationUpdated);

            //Central Management System
            //var result = ExceptionHandler.HandleWithNoReturn(() =>
            //{
            //    _locationDal.Update(location);
            //});
            //if (!result)
            //{
            //    return new ErrorResult(TurkishMessage.ErrorMessage);
            //}

            //return new SuccessResult(TurkishMessage.LocationUpdated);
        }

        private IResult CheckIfLocationNameExists(string locationName)
        {
            var result = _locationDal.GetAll(loc => loc.Name == locationName).Any();
            if (result)
            {
                return new ErrorResult(TurkishMessage.LocationNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
