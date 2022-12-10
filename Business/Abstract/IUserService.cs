using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<RoleType> GetClaim(User user);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int userId);
        IDataResult<User> GetByMail(string email);
        IDataResult<UserForView> GetUserForView(User user);
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(UserForInfoChange userForInfoChange);

        IResult ChangeUserRole(int userId, int roleId);

        //IDataResult<List<UserDetailDto>> GetUserDetails();
        IDataResult<User> ChangePassword(string oldPassword, string newPassowrd);

    }
}
