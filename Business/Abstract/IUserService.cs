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
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
        IDataResult<User> GetByMail(string email);
        //IDataResult<List<UserDetailDto>> GetUserDetails();
        IDataResult<UserForView> GetUserForView(User user);

        //IDataResult<UserForLoginDto> GetByUserForLogin();


        //IDataResult<User> ChangePassword(string password);

    }
}
