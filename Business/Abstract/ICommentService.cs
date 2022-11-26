using Core.Utilities.Results.Abstract;
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
        IDataResult<Comment> GetById(int commentId);
        IResult Add(Comment comment);
        IResult Delete(Comment comment  );
        IResult Update(Comment comment);
    }
}
