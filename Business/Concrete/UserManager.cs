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

        public IDataResult<RoleType> GetClaim(User user)
        {
            var result = _userDal.GetClaim(user);
            return new SuccessDataResult<RoleType>(result, TurkishMessage.SuccessMessage);
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

        public IResult Delete(User user)
        {
            //Business Codes


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Delete(user);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }
            return new SuccessResult(TurkishMessage.SuccessMessage);
        }

        public IResult Update(User user)
        {
            //Business Codes


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Update(user);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }
            return new SuccessResult(TurkishMessage.SuccessMessage);
        }


        public IDataResult<User> GetByMail(string email)
        {

            var result = _userDal.Get(u => u.Email == email);
            return new SuccessDataResult<User>(result, TurkishMessage.SuccessMessage);
        }


        public IDataResult<List<User>> GetAll()
        {
            //Business code


            //Cetnral Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<User>>(() =>
            {
                return _userDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<User>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<User>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        public IDataResult<UserForView> GetUserForView(User user)
        {
            var userForView = new UserForView()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                Role = user.RoleType.RoleName
            };

            return new SuccessDataResult<UserForView>(userForView);
        }

        public IDataResult<User> GetById(int userId)
        {
            var result = _userDal.Get(u => u.Id == userId);
            return new SuccessDataResult<User>(result, TurkishMessage.UserInfoListed);
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
