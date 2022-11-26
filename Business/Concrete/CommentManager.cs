using Business.Abstract;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

        [ValidationAspect(typeof(CommentValidator))]
        public IResult Add(Comment comment)
        {
            //Business code



            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _commentDal.Add(comment);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CommentAdded);
        }

        public IResult Delete(Comment comment)
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _commentDal.Delete(comment);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CommentDeleted);
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

        public IDataResult<Comment> GetById(int commentId)
        {
            //Business code



            //Central Management System
            var result = ExceptionHandler.HandleWithReturn<int, Comment>((int x) =>
            {
                return _commentDal.Get(c => c.Id == x);
            }, commentId);
            if (!result.Success)
            {
                return new ErrorDataResult<Comment>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<Comment>(result.Data, TurkishMessage.SuccessMessage);
        }


        [ValidationAspect(typeof(CommentValidator))]
        public IResult Update(Comment comment)
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _commentDal.Update(comment);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.CommentUpdated);
        }
    }
}
