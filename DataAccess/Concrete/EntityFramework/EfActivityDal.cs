using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
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
    public class EfActivityDal : EfEntityRepositoryBase<Activity, ActivityContext> , IActivityDal
    {

        // public List<Activity> GetAll() {
        //     using (ActivityContext context = new ActivityContext()) {
        //         var result = context.Activities.Join(context.Locations, activity => activity.LocationId, loc => loc.Id, )
        //     }
        // }

        public List<ActivityForView> GetAll() //GetAll
        {
            using (ActivityContext context = new ActivityContext())
            {
                // get upcoming activities
                var now = DateTime.Now.Date;
                var result = context.Activities.Where(x => x.ActivityDate > now).Join(context.Locations, activity => activity.LocationId, loc => loc.Id, (activity, loc) => new
                {
                    activity,
                    loc = new LocationInfoForActivities
                    {
                        Id = loc.Id,
                        CityId = loc.City.Id,
                        CountryId = loc.City.Country.Id,
                        Name = loc.Name,
                        CityName = loc.City.CityName,
                        CountryName = loc.City.Country.CountryName
                    }
                }).Join(context.Users, activity => activity.activity.UserId, user => user.Id, (activity, user) => new {
                    activity,
                    user = new UserInfoForActivities
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Image = user.UserPhoto,
                        Role = user.RoleType.RoleName
                    }
                }).Select(x => new ActivityForView
                {
                    Id = x.activity.activity.Id,
                    Title = x.activity.activity.Title,
                    Description = x.activity.activity.Description,
                    Image = x.activity.activity.Image,
                    Participiant = x.activity.activity.Participiant,
                    CreatedTime = x.activity.activity.CreatedTime,
                    ActivityDate = x.activity.activity.ActivityDate,
                    Location = x.activity.loc,
                    ActivityType = x.activity.activity.ActivityType,
                    User = x.user
                });
                
                return result.ToList();
            }
        }

        public ActivityForView GetById(int activityId) {
            using (ActivityContext context = new ActivityContext()) {
                var result = context.Activities.Join(context.Locations, activity => activity.LocationId, loc => loc.Id, (activity, loc) => new
                {
                    activity,
                    loc = new LocationInfoForActivities
                    {
                        Id = loc.Id,
                        Name = loc.Name,
                        CityId = loc.City.Id,
                        CountryId = loc.City.Country.Id,
                        CityName = loc.City.CityName,
                        CountryName = loc.City.Country.CountryName
                    }
                }).Join(context.Users, activity => activity.activity.UserId, user => user.Id, (activity, user) => new
                {
                    activity,
                    user = new UserInfoForActivities
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Image = user.UserPhoto,
                        Role = user.RoleType.RoleName
                    }
                }).Where(x => x.activity.activity.Id == activityId).Select(x => new ActivityForView
                {
                    Id = x.activity.activity.Id,
                    Title = x.activity.activity.Title,
                    Description = x.activity.activity.Description,
                    Image = x.activity.activity.Image,
                    Participiant = x.activity.activity.Participiant,
                    CreatedTime = x.activity.activity.CreatedTime,
                    ActivityDate = x.activity.activity.ActivityDate,
                    Location = x.activity.loc,
                    ActivityType = x.activity.activity.ActivityType,
                    User = x.user
                });

                return result.SingleOrDefault();
            }   
        }

        //public List<User> GetParticipiantstoActivity(int activityId)
        //{
        //    using (ActivityContext context = new ActivityContext)
        //    {
        //        var result = context.Registrations.SingleOrDefault(r => r.ActivityId == activityId);
        //        return result.ToList();
        //    }
        //}
    }
}
