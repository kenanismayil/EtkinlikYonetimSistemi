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
    public interface ICertificateService
    {
        IDataResult<List<Certificate>> GetAll();
        IDataResult<Certificate> GetById(int id);
        IResult Add(Certificate certificate);
        IResult Delete(Certificate certificate);
        IResult DeleteAll(Expression<Func<Certificate, bool>> filter);
        IResult Update(Certificate certificate);
    }
}
