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
    public interface ICertificateService
    {
        IDataResult<List<Certificate>> GetAll();
        IDataResult<Certificate> GetByCertificateId(int certificateId);
        //IDataResult<Certificate> GetByUserId(int userId);
        IResult Add(CertificateForView certificate, string pnrNo);
        IResult Delete(int certificateId);
        IResult Update(CertificateForView certificate);
    }
}
