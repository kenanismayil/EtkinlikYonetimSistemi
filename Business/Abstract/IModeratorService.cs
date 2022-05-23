using Core.Utilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IModeratorService
    {
        IDataResult<List<Moderator>> GetAll();
        IDataResult<Moderator> GetById(int id);
        IResult Add(Moderator moderator);
        IResult Delete(Moderator moderator);
        IResult DeleteAll(Expression<Func<Moderator, bool>> filter);
        IResult Update(Moderator moderator);
    }
}
