using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
        // IDataResult<Comment> GetById(int commentId);
        IDataResult<List<CommentForView>> GetByActivityId(int activityId);
        IResult Add(CommentForUser comment, string token);
        IResult Delete(string commentId);
        IResult Update(CommentForUser comment, string token);
    }
}
