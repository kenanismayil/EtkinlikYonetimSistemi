using Business.Abstract;
using Business.Constants.Messages;
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
    public class ModeratorManager : IModeratorService 
    {
        IModeratorDal _moderatorDal;
        public ModeratorManager(IModeratorDal moderatorDal)
        {
            _moderatorDal = moderatorDal;
        }

        public IResult Add(Moderator moderator)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _moderatorDal.Add(moderator);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ModeratorAdded);
        }

        public IResult Delete(Moderator moderator)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _moderatorDal.Delete(moderator);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ModeratorDeleted);
        }

        public IResult DeleteAll(Expression<Func<Moderator, bool>> filter)
        {
            //Business code
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _moderatorDal.DeleteAll(filter);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ModeratorDeleted);
        }

        public IDataResult<List<Moderator>> GetAll()
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Moderator>>(() =>
            {
                return _moderatorDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Moderator>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<Moderator>>(result.Data, TurkishMessage.ModeratorsListed);
        }

        public IDataResult<Moderator> GetById(int id)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, Moderator>((int x) =>
            {
                return _moderatorDal.Get(m => m.Id == x);
            }, id);
            if (!result.Success)
            {
                return new ErrorDataResult<Moderator>(TurkishMessage.ErrorMessage);
            }


            return new SuccessDataResult<Moderator>(result.Data, TurkishMessage.SuccessMessage);
        }


        public IResult Update(Moderator moderator)
        {
            //Business code
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorResult(TurkishMessage.MaintenanceTime);
            }

            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _moderatorDal.Update(moderator);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ModeratorUpdated);
        }
    }
}
