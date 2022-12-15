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

        IResult UpdateUserInfoBySuperAdmin(User user);

        //IDataResult<List<UserDetailDto>> GetUserDetails();
        IResult ChangePassword(int Id, string oldPassword, string newPassword);  //Şifre yenileme 

    }
}
