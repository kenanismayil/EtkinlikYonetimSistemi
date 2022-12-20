using Business.Abstract;
using Business.Constants.Messages;
using Business.Helper;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
        IAuthHelper _authHelper;
        public CommentManager(ICommentDal commentDal, IAuthHelper authHelper)
        {
            _commentDal = commentDal;
            _authHelper = authHelper;
        }

        [ValidationAspect(typeof(CommentValidator))]
        public IResult Add(CommentForUser comment, string token)
        {
            //Business code
            var currentUserId = _authHelper.GetCurrentUser(token).Data.Id;
            var commentData = new Comment()
            {
                UserId = currentUserId,
                ActivityId = comment.ActivityId,
                Content = comment.Content
            };


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _commentDal.Add(commentData);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CommentAdded);
        }

        public IResult Delete(string commentId)
        {
            //Business code
            var commentData = _commentDal.Get(c => c.Id.ToString() == commentId);

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _commentDal.Delete(commentData);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CommentDeleted);
        }

        [ValidationAspect(typeof(CommentValidator))]
        public IResult Update(CommentForUser comment, string token)
        {
            //Business code
            var currentUserId = _authHelper.GetCurrentUser(token).Data.Id;

            var commentData = new Comment()
            {
                Id = comment.Id,
                UserId = currentUserId,
                ActivityId = comment.ActivityId,
                Content = comment.Content
            };

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _commentDal.Update(commentData);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CommentUpdated);
        }

        public IDataResult<List<Comment>> GetAll()
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Comment>>(() =>
            {
                return _commentDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Comment>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<Comment>>(result.Data, TurkishMessage.CommentsListed);
        }

        public IDataResult<List<CommentForView>> GetByActivityId(int activityId)
        {
            //Business code

            //Central Management System
            var result = ExceptionHandler.HandleWithReturn<int, List<CommentForView>>((int x) =>
            {
                return _commentDal.GetByActivityId(x);
            }, activityId);

            if (!result.Success)
            {
                return new ErrorDataResult<List<CommentForView>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<CommentForView>>(result.Data, TurkishMessage.SuccessMessage);
        }
    }
}
