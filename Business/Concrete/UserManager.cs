using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.BusinessRuleHandle;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(result, TurkishMessage.SuccessMessage);
        }

        [CacheRemoveAspect("IUserService.Get")]
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            //Business Codes


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Add(user);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }
            return new SuccessResult(TurkishMessage.SuccessMessage);
        }

        public IDataResult<User> GetByMail(string email)
        {

            //Business Codes
            //var result = _userDal.Get(u => u.Email == email);
            //if (result == null)
            //{
            //    return new ErrorDataResult<User>(TurkishMessage.UserNotFound);
            //}
            //return new SuccessDataResult<User>(result, TurkishMessage.SuccessMessage);


            //Central Management System
            //var result = ExceptionHandler.HandleWithReturn<string, User>((email) =>
            //{
            //    return _userDal.GetAll(u => u.Email == email)[0];
            //}, email);
            //if (!result.Success)
            //{
            //    return new ErrorDataResult<User>(TurkishMessage.ErrorMessage);
            //}

            var result = _userDal.Get(p => p.Email == email);
            return new SuccessDataResult<User>(result, TurkishMessage.SuccessMessage);
        }

        public IDataResult<List<UserDetailDto>> GetUserDetails()
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<UserDetailDto>>(() =>
            {
                return _userDal.GetUserDetails();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<UserDetailDto>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<UserDetailDto>>(result.Data, TurkishMessage.UserDetailListed);
        }


        //İŞ KURALLARI

        //private IResult UserEmailControl(string email)
        //{
        //    var result = _userDal.GetAll(u => u.Email == email).Any();
        //    if (result)
        //    {
        //        return new ErrorResult("Sisteme varolan kullanici emaili eklenemez");
        //    }

        //    return new SuccessResult(TurkishMessage.SuccessMessage);
        //}

        //private IResult UserPhoneControl(string phoneNumber)
        //{
        //    var result = _userDal.GetAll(u => u.Phone == phoneNumber).Any();
        //    if (result)
        //    {
        //        return new ErrorResult("Sisteme varolan kullanici numarasi eklenemez");
        //    }

        //    return new SuccessResult(TurkishMessage.SuccessMessage);
        //}
    }
}
