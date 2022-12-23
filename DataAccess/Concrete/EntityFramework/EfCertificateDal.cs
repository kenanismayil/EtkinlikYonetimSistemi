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
    public class EfCertificateDal : EfEntityRepositoryBase<Certificate, ActivityContext>, ICertificateDal
    {
        public UserCertificateInfo GetCertificates(int activityId, int userId)
        {
            using (var context = new ActivityContext())
            {
                var result = context.Certificates.Where(c => c.ActivityId == activityId && c.UserId == userId).Join(context.Activities, cer => cer.ActivityId, act => act.Id, (cer, act) => new
                {
                    cer,
                    activity = new ActivityForCertificate
                    {
                        ActivityId = act.Id,
                        Title = act.Title
                    }
                }).Join(context.Users, cer => cer.cer.UserId, user => user.Id, (cer, user) => new
                {
                    cer,
                    user = new UserForCertificate
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,   
                    }

                }).Where(x => x.cer.cer.ActivityId == activityId && x.cer.cer.UserId == userId).
                Select(x => new UserCertificateInfo
                {
                    UserId = x.cer.cer.User.Id,
                    ActivityId = x.cer.cer.Activity.Id,
                    ActivityName = x.cer.cer.Activity.Title,
                    FirstName = x.cer.cer.User.FirstName,
                    LastName = x.cer.cer.User.LastName,
                    GivenDate = x.cer.cer.GivenDate,
                    ExpiryDate = x.cer.cer.ExpiryDate
                });

                return result.SingleOrDefault();
            }
        }

        public List<UserCertificateInfo> GetCertificatesForUser(int userId)
        {
            using (var context = new ActivityContext())
            {
                var result = context.Certificates.Where(c => c.UserId == userId).Join(context.Activities, cer => cer.ActivityId, act => act.Id, (cer, act) => new
                {
                    cer,
                    user = new UserForCertificate
                    {
                        UserId = act.User.Id,
                        FirstName = act.User.FirstName,
                        LastName = act.User.LastName,
                    },

                    activity = new ActivityForCertificate
                    {
                        ActivityId = act.Id,
                        Title = act.Title
                    }

                }).
                Select(x => new UserCertificateInfo
                {
                    UserId = x.cer.User.Id,
                    ActivityId = x.cer.Activity.Id,
                    ActivityName = x.cer.Activity.Title,
                    FirstName = x.cer.User.FirstName,
                    LastName = x.cer.User.LastName,
                    GivenDate = x.cer.GivenDate,
                    ExpiryDate = x.cer.ExpiryDate
                }).ToList();

                return result;
            }
        }

        public UserCertificateInfo GetByCertificateId(int certificateId)
        {
            using (var context = new ActivityContext())
            {
                var result = context.Certificates.Where(c => c.Id == certificateId).Join(context.Activities, cer => cer.ActivityId, act => act.Id, (cer, act) => new
                {
                    cer,
                    activity = new ActivityForCertificate
                    {
                        ActivityId = act.Id,
                        Title = act.Title
                    }
                }).Join(context.Users, cer => cer.cer.UserId, user => user.Id, (cer, user) => new
                {
                    cer,
                    user = new UserForCertificate
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    }

                }).
                Select(x => new UserCertificateInfo
                {
                    UserId = x.cer.cer.User.Id,
                    ActivityId = x.cer.cer.Activity.Id,
                    ActivityName = x.cer.cer.Activity.Title,
                    FirstName = x.cer.cer.User.FirstName,
                    LastName = x.cer.cer.User.LastName,
                    GivenDate = x.cer.cer.GivenDate,
                    ExpiryDate = x.cer.cer.ExpiryDate
                });

                return result.SingleOrDefault();
            }
        }
    }

}
