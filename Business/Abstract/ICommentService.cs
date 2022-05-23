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
    public interface ICommentService
    {
        IDataResult<List<Comment>> GetAll();
        IDataResult<Comment> GetById(int id);
        IResult Add(Comment comment);
        IResult Delete(Comment comment  );
        IResult DeleteAll(Expression<Func<Comment, bool>> filter);
        IResult Update(Comment comment);
    }
}
