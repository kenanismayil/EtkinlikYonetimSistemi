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
    public class EfCommentDal : EfEntityRepositoryBase<Comment, ActivityContext>, ICommentDal
    {
        public List<CommentForView> GetByActivityId(int activityId)
        {
            using (ActivityContext context = new ActivityContext())
            {
                var result = context.Comments.Join(context.Users, c => c.UserId, u => u.Id, (c, u) => new
                {
                    Id = c.Id,
                    Content = c.Content,
                    ActivityId = c.ActivityId,
                    User = new UserInfoForComments
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Image = u.UserPhoto,
                    }

                }).Where(c => c.ActivityId == activityId).Select(c => new CommentForView
                {
                    Id = c.Id,
                    Content = c.Content,
                    User = c.User
            });

                return result.ToList();
            }
        }
    }
}
