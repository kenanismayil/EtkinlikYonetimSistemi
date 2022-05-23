using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfActivityDal : EfEntityRepositoryBase<Activity, ActivityContext>, IActivityDal
    {
        public List<ActivityDetailDto> GetActivityDetails()
        {
            using (ActivityContext context = new ActivityContext())
            {
                var result = from t in context.ActivityTypes
                             join a in context.Activities 
                             on t.Id equals a.ActivityTypeId
                             join c in context.Cities
                             on a.CityId equals c.Id
                             select new ActivityDetailDto
                             {
                                 ActivityId = a.Id, ActivityName = a.ActivityName, ActivityDate = a.ActivityDate,
                                 CityName = c.CityName, ActivityTypeName = t.ActivityName
                             };
                return result.ToList();

            }
        }
    }
}
