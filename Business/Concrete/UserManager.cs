using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.Constants.Messages;
using Business.Constants.PathConstants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.BusinessRuleHandle;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
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
        IFileHelper _fileHelper;


        public UserManager(IUserDal userDal, IFileHelper fileHelper)
        {
            _userDal = userDal;
            _fileHelper = fileHelper;
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

        public IResult Delete(string id)
        {
            //Business Codes
            var user = _userDal.Get(u => u.Id.ToString() == id);

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

        //[SecuredOperation("super_admin")]
        public IResult Update(UserForInfoChange userForInfoChange, IFormFile file)
        {
            //Business Codes
            var userData = _userDal.Get(u => u.Id == userForInfoChange.Id);

            userData.FirstName = userForInfoChange.FirstName;
            userData.LastName = userForInfoChange.LastName;
            userData.Email = userForInfoChange.Email;
            userData.DateOfBirth = userForInfoChange.DateOfBirth;
            userData.Phone = userForInfoChange.Phone;

            userForInfoChange.UserPhoto = _fileHelper.Update(file, PathConstants.ImagesPath + userForInfoChange.UserPhoto, PathConstants.ImagesPath);
            userData.UserPhoto = userForInfoChange.UserPhoto;

            _userDal.Update(userData);

            return new SuccessResult(TurkishMessage.UserUpdated);
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
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                Role = user.RoleType.RoleName,
                UserPhoto = user.UserPhoto
            };

            return new SuccessDataResult<UserForView>(userForView);
        }

        public IDataResult<User> GetById(int userId)
        {
            var result = _userDal.Get(u => u.Id == userId);
            return new SuccessDataResult<User>(result, TurkishMessage.UserInfoListed);
        }

        public IResult UpdateUserInfoBySuperAdmin(User user)
        {

            var userData = _userDal.Get(u => u.Id == user.Id);

            userData.FirstName = user.FirstName;
            userData.LastName = user.LastName;
            userData.Email = user.Email;
            userData.DateOfBirth = user.DateOfBirth;
            userData.Phone = user.Phone;
            userData.RoleTypeId = user.RoleTypeId;

            _userDal.Update(userData);
            return new SuccessResult(TurkishMessage.UserRoleUpdatedBySuperAdmin);
        }

        public IResult ChangePassword(int Id, string oldPassword, string newPassword)
        {
            byte[] oldPasswordHash, oldPasswordSalt;
            byte[] newPasswordHash, newPasswordSalt;

            HashingHelper.CreatePasswordHash(oldPassword, out oldPasswordHash, out oldPasswordSalt);
            var userData = _userDal.Get(u=>u.Id == Id);

            HashingHelper.CreatePasswordHash(newPassword, out newPasswordHash, out newPasswordSalt);

            if (HashingHelper.VerifyPasswordHash(oldPassword, userData.PasswordHash, userData.PasswordSalt))
            {
                userData.PasswordHash = newPasswordHash;
                userData.PasswordSalt = newPasswordSalt;
            }
            else
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }



            _userDal.Update(userData);
            return new SuccessResult(TurkishMessage.SuccessMessage);
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
