using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Business.Concrete
{
    public class CommentManager : ICommentService 
    {
        ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public IResult Add(Comment comment)
        {
            //Business code

            _commentDal.Add(comment);
            return new SuccessResult(Message.CommentAdded);
        }

        public IResult Delete(Comment comment)
        {
            //Business code

            _commentDal.Delete(comment);
            return new SuccessResult(Message.CommentDeleted);
        }

        public IResult DeleteAll(Expression<Func<Comment, bool>> filter)
        {
            //Business code

            _commentDal.DeleteAll(filter);
            return new SuccessResult(Message.CommentDeleted);
        }

        public IDataResult<List<Comment>> GetAll()
        {
            //Business code

            var data = _commentDal.GetAll();
            return new SuccessDataResult<List<Comment>>(data, Message.ActivitiesListed);
        }

        public IDataResult<Comment> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Comment>(Message.MaintenanceTime);
            }

            var data = _commentDal.Get(c => c.Id == id);
            return new SuccessDataResult<Comment>(data);
        }


        public IResult Update(Comment comment)
        {
            //Business code

            _commentDal.Update(comment);
            return new SuccessResult(Message.CommentUpdated);
        }
    }
}
