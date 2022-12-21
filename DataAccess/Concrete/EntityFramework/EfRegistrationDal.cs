﻿using Core.DataAccess.EntityFramework;
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
    public class EfRegistrationDal : EfEntityRepositoryBase<Registration, ActivityContext>, IRegistrationDal
    {
        //public List<User> GetClaim(int activityId)
        //{
        //    using (var context = new ActivityContext())
        //    {

        //        return result;
        //    }
        //}
        public List<UserRegisteredEventsInfo> GetRegisteredEvents(int userId) 
        {
            using (var context = new ActivityContext())
            {
                var result = context.Registrations.Where(x => x.UserId == userId).Join(context.Activities, reg => reg.ActivityId, act => act.Id, (reg, act) => new
                {
                    reg,
                    act = new ActivityForView
                    {
                        Id = act.Id,
                        Title = act.Title,
                        Description = act.Description,
                        Image = act.Image,
                        Participiant = act.Participiant,
                        CreatedTime = act.CreatedTime,
                        ActivityDate = act.ActivityDate,
                        Location = new LocationInfoForActivities
                        {
                            Id = act.Location.Id,
                            Name = act.Location.Name,
                            CityName = act.Location.City.CityName,
                            CountryName = act.Location.City.Country.CountryName
                        },
                        ActivityType = act.ActivityType,
                        User = new UserInfoForActivities
                        {
                            Id = act.User.Id,
                            FirstName = act.User.FirstName,
                            LastName = act.User.LastName,
                            Image = act.User.UserPhoto,
                            Role = act.User.RoleType.RoleName
                        }
                    }
                }).Select(x => new UserRegisteredEventsInfo
                {
                    Id = x.reg.Id,
                    UserId = x.reg.User.Id,
                    ActivityId = x.reg.Activity.Id,
                    Date = x.reg.Date,
                    Activity = x.act
                }).ToList();
                
                return result;
            }
        }
    }
}
