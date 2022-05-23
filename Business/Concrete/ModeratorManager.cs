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

            _moderatorDal.Add(moderator);
            return new SuccessResult(Message.ModeratorAdded);
        }

        public IResult Delete(Moderator moderator)
        {
            //Business code

            _moderatorDal.Delete(moderator);
            return new SuccessResult(Message.ModeratorDeleted);
        }

        public IResult DeleteAll(Expression<Func<Moderator, bool>> filter)
        {
            //Business code

            _moderatorDal.DeleteAll(filter);
            return new SuccessResult(Message.ModeratorDeleted);
        }

        public IDataResult<List<Moderator>> GetAll()
        {
            //Business code

            var data = _moderatorDal.GetAll();
            return new SuccessDataResult<List<Moderator>>(data, Message.ModeratorsListed);
        }

        public IDataResult<Moderator> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Moderator>(Message.MaintenanceTime);
            }

            var data = _moderatorDal.Get(c => c.Id == id);
            return new SuccessDataResult<Moderator>(data);
        }


        public IResult Update(Moderator moderator)
        {
            //Business code

            _moderatorDal.Update(moderator);
            return new SuccessResult(Message.ModeratorUpdated);
        }
    }
}
