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
                
                var result = from a in context.Activities
                             join t in context.ActivityTypes
                             on a.ActivityTypeId equals t.Id
                             join loc in context.Locations
                             on a.LocationId equals loc.Id
                             join city in context.Cities
                             on loc.CityId equals city.Id
                             join country in context.Countries
                             on city.CountryId equals country.Id
                             join user in context.Users
                             on a.UserId equals user.Id
                             select new ActivityDetailDto
                             {
                                 ActivityId = a.Id, ActivityName = a.ActivityName, 
                                 CreatedTime = a.CreatedTime, AppDeadLine = a.AppDeadLine, ActivityDate = a.ActivityDate, 
                                 ActivityTypeName = t.ActivityTypeName,
                                 CountryName = country.CountryName,
                                 CityName = city.CityName,
                                 LocationName = loc.Name,
                             };
                return result.ToList();
            }
        }
    }
}
