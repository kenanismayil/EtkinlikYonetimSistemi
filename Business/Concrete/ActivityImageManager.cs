using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.Constants.Messages;
using Business.Constants.PathConstants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ActivityImageManager : IActivityImageService
    {
        IActivityImageDal _activityImageDal;
        IFileHelper _fileHelper;

        public ActivityImageManager(IActivityImageDal activityImageDal, IFileHelper fileHelper)
        {
            _activityImageDal = activityImageDal;
            _fileHelper = fileHelper;
        }

        [ValidationAspect(typeof(ActivityImageValidator))]
        [CacheRemoveAspect("IActivityImageService.Get")]
        public IResult Add(IFormFile formFile, ActivityImage activityImage, string rootPath)
        {
            //Bussiness codes
            var result = BusinessRules.Run(CheckIfActivityImageLimitExceded(activityImage.ActivityId));

            if (result != null)
            {
                return result;
            }

            _fileHelper.Upload(formFile, Path.Combine(rootPath, PathConstants.ImagesPath)); // bu combine metodu ozu slas qoyur
            activityImage.Date = DateTime.Now;
            _activityImageDal.Add(activityImage);
            return new SuccessResult(TurkishMessage.ActivityImageAdded);
        }

        [CacheRemoveAspect("IActivityImageService.Get")]
        public IResult Delete(ActivityImage activityImage, string rootPath)
        {
            _fileHelper.Delete(PathConstants.ImagesPath + activityImage.ImagePath);
            _activityImageDal.Delete(activityImage);
            return new SuccessResult(TurkishMessage.ActivityImageDeleted);
        }


        [CacheAspect]
        public IDataResult<List<ActivityImage>> GetAll()
        {
            //Business code


            //Central Management System
            //var result = ExceptionHandler.HandleWithReturnNoParameter<List<ActivityImage>>(() =>
            //{
            //    _activityImageDal.GetAll();
            //});
            //if (!result.Success)
            //{
            //    return new ErrorDataResult<List<ActivityImage>>(TurkishMessage.ErrorMessage);
            //}

            var result =_activityImageDal.GetAll();
            return new SuccessDataResult<List<ActivityImage>>(result, TurkishMessage.ActivityImageListed);
        }


        [CacheAspect]
        public IDataResult<ActivityImage> GetImageByActivityId(int activityId)
        {
            //Business code
            //var result = BusinessRules.Run(CheckIfActivityImageAvailable(activityId));

            //if (result != null)
            //    return new ErrorDataResult<List<ActivityImage>>(CreateDefaultActivityImage().Data);

            //return new SuccessDataResult<List<ActivityImage>>(_activityImageDal.GetAll(image => image.ActivityId == activityId));


            //Central Management System
            var result = ExceptionHandler.HandleWithReturn<int, ActivityImage>((x) =>
            {
                return _activityImageDal.Get(a => a.Id == x);
            }, activityId);
            if (!result.Success)
            {
                return new ErrorDataResult<ActivityImage>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<ActivityImage>(result.Data, TurkishMessage.SuccessMessage);
        }

        [ValidationAspect(typeof(ActivityImageValidator))]
        [CacheRemoveAspect("IActivityImageService.Get")]
        public IResult Update(IFormFile formFile, ActivityImage activityImage, string rootPath)
        {
            _fileHelper.Update(formFile, activityImage.ImagePath, Path.Combine(rootPath, PathConstants.ImagesPath));
            activityImage.Date = DateTime.Now;
            _activityImageDal.Update(activityImage);
            return new SuccessResult(TurkishMessage.ActivityImageUpdated);
        }



        //Bir aktiviteye maksimum 5 resim eklenebilir.
        private IResult CheckIfActivityImageLimitExceded(int activityId)
        {
            var result = _activityImageDal.GetAll(image => image.ActivityId == activityId).Count;

            if (result > 5)
                return new ErrorResult(TurkishMessage.ActivityImageLimitExceded);

            return new SuccessResult();
        }

        private IResult CheckIfActivityImageAvailable(int activityId)
        {
            var result = _activityImageDal.GetAll(image => image.ActivityId == activityId).Count;

            if (result > 0)
                return new SuccessResult();

            return new ErrorResult();
        }

        //private IDataResult<List<ActivityImage>> CreateDefaultActivityImage()
        //{
        //    List<ActivityImage> activityImages = new()
        //    {
        //        new() { Date = DateTime.Now, ImagePath = "default-activity.jpg" }
        //    };
        //    return new SuccessDataResult<List<ActivityImage>>(activityImages);
        //}
    }
}
