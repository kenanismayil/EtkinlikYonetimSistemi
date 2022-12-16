using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRegistrationDal : EfEntityRepositoryBase<Registration, ActivityContext>, IRegistrationDal
    {
        //public List<User> GetClaim(int activityId)
        //{
        //    using (var context = new ActivityContext())
        //    {

        //        return result;
        //    }
        //}
    }
}
