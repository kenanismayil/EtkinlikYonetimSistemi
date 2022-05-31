using Business.Abstract;
using Business.Constants.Messages;
using Core.BusinessRuleHandle;
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
    public class UserManager : IUserService 
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            //Business code
            var ruleExceptions = BusinessRuleHandler.CheckTheRules(UserEmailControl(user), UserPhoneControl(user));
            if (!ruleExceptions.Success)
            {
                return new ErrorResult(ruleExceptions.Message);
            }


            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Add(user);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.UserAdded);
        }

        public IResult Delete(User user)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Delete(user);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.UserDeleted);
        }

        public IResult DeleteAll(Expression<Func<User, bool>> filter)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.DeleteAll(filter);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            //Business code
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

        public IDataResult<User> GetById(int id)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, User>((int x) =>
            {
                return _userDal.Get(u => u.Id == x);
            }, id);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<User>(result.Data, TurkishMessage.SuccessMessage);
        }


        public IResult Update(User user)
        {
            //Business codes
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorResult(TurkishMessage.MaintenanceTime);
            }

            var ruleExceptions = BusinessRuleHandler.CheckTheRules(UserEmailControl(user), UserPhoneControl(user));
            if (!ruleExceptions.Success)
            {
                return new ErrorResult(ruleExceptions.Message);
            }

            //Central Error Management
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _userDal.Update(user);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.UserUpdated);
        }

        private IResult UserEmailControl(User user)
        {
            var users = _userDal.Get(u => u.Email == user.Email);
            if (users != null)
            {
                return new ErrorResult("Sisteme varolan kullanici emaili eklenemez");
            }

            return new SuccessResult(TurkishMessage.SuccessMessage);
        }

        private IResult UserPhoneControl(User user)
        {
            var users = _userDal.Get(u => u.Phone == user.Phone);
            if (users != null)
            {
                return new ErrorResult("Sisteme varolan kullanici numarasi eklenemez");
            }

            return new SuccessResult(TurkishMessage.SuccessMessage);
        }

        
    }
}
