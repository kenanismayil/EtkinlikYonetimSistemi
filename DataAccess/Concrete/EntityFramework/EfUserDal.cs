using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ActivityContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ActivityContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim 
                             { 
                                 Id = operationClaim.Id, Name = operationClaim.Name 
                             };
                return result.ToList();
            }
        }

        public List<UserDetailDto> GetUserDetails()
        {
            using (ActivityContext context = new ActivityContext())
            {

                var result = from u in context.Users
                             join a in context.Activities
                             on u.Id equals a.UserId
                             join type in context.ActivityTypes
                             on a.ActivityTypeId equals type.Id
                             select new UserDetailDto
                             {
                                 UserId = u.Id, UserName = u.FirstName, UserSurname = u.LastName, 
                                 UserPhoto = u.UserPhoto, DateOfBirth = u.DateOfBirth, 
                                 Email = u.Email, Phone = u.Phone,
                                 ActivityId = a.Id, ActivityName = a.ActivityName, 
                                 ActivityTypeId = type.Id, ActivityTypeName = type.ActivityTypeName, 
                             };
                return result.ToList();
            }
        }
    }
}
