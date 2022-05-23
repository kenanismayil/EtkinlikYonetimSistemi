using Business.Abstract;
using Business.Constants.Messages;
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

            _userDal.Add(user);
            return new SuccessResult(Message.UserAdded);
        }

        public IResult Delete(User user)
        {
            //Business code

            _userDal.Delete(user);
            return new SuccessResult(Message.UserDeleted);
        }

        public IResult DeleteAll(Expression<Func<User, bool>> filter)
        {
            //Business code

            _userDal.DeleteAll(filter);
            return new SuccessResult(Message.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            //Business code

            var data = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(data, Message.ActivitiesListed);
        }

        public IDataResult<User> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<User>(Message.MaintenanceTime);
            }

            var data = _userDal.Get(c => c.Id == id);
            return new SuccessDataResult<User>(data);
        }


        public IResult Update(User user)
        {
            //Business code
            
            _userDal.Update(user);
            return new SuccessResult(Message.UserUpdated);
        }
    }
}
