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
    public class ActivityTypeManager : IActivityTypeService
    {
        IActivityTypeDal _activityTypeDal;
        public ActivityTypeManager(IActivityTypeDal activityTypeDal)
        {
            _activityTypeDal = activityTypeDal;
        }

        [ValidationAspect(typeof(ActivityTypeValidator))]
        [CacheRemoveAspect("IActivityTypeService.Get")]
        public IResult Add(ActivityType activityType)
        {
            //Business codes
            IResult result = BusinessRules.Run(CheckIfActivityTypeNameExists(activityType.ActivityTypeName));
            if (result != null)
            {
                return result;
            }

            _activityTypeDal.Add(activityType);
            return new SuccessResult(TurkishMessage.ActivityTypeAdded);


            //var result = ExceptionHandler.HandleWithNoReturn(() =>
            //{
            //    _activityTypeDal.Add(activityType);
            //});
            //if (!result)
            //{
            //    return new ErrorResult(TurkishMessage.ErrorMessage);
            //}

            //return new SuccessResult(TurkishMessage.ActivityTypeAdded);
        }

        [CacheRemoveAspect("IActivityTypeService.Get")]
        public IResult Delete(ActivityType activityType)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityTypeDal.Delete(activityType);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityDeleted);
        }

        [CacheRemoveAspect("IActivityTypeService.Get")]
        public IResult DeleteAll(Expression<Func<ActivityType, bool>> filter)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityTypeDal.DeleteAll(filter);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityTypeDeleted);
        }

        [CacheAspect]
        public IDataResult<List<ActivityType>> GetAll()
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<ActivityType>>(() =>
            {
                return _activityTypeDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<ActivityType>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<ActivityType>>(result.Data, TurkishMessage.ActivityTypesListed);
        }

        [CacheAspect]
        public IDataResult<ActivityType> GetById(int activityTypeId)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, ActivityType>((x) =>
            {
                return _activityTypeDal.Get(a => a.Id == x);
            }, activityTypeId);
            if (!result.Success)
            {
                return new ErrorDataResult<ActivityType>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<ActivityType>(result.Data, TurkishMessage.SuccessMessage);
        }


        [ValidationAspect(typeof(ActivityTypeValidator))]
        [CacheRemoveAspect("IActivityTypeService.Get")]
        public IResult Update(ActivityType activityType)
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityTypeDal.Update(activityType);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityUpdated);
        }



        //İş kuralları
        private IResult CheckIfActivityTypeNameExists(string activityTypeName)
        {
            //Aynı isimde aktivite tipi eklenemez
            var result = _activityTypeDal.GetAll(a => a.ActivityTypeName == activityTypeName).Any();
            if (result)
            {
                return new ErrorResult(TurkishMessage.ActivityTypeNameAlreadyExists);
            }
            return new SuccessResult();
        }

    }
}
