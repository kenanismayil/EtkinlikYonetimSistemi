using Business.Abstract;
using Business.Constants.Messages;
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
    public class CertificateManager : ICertificateService
    {
        ICertificateDal _certificateDal;
        public CertificateManager(ICertificateDal certificateDal)
        {
            _certificateDal = certificateDal;
        }


        public IResult Add(Certificate certificate)
        {
            //Business code

            _certificateDal.Add(certificate);
            return new SuccessResult(Message.CertificateAdded);
        }

        public IResult Delete(Certificate certificate)
        {
            //Business code

            _certificateDal.Delete(certificate);
            return new SuccessResult(Message.ActivityDeleted);
        }

        public IResult DeleteAll(Expression<Func<Certificate, bool>> filter)
        {
            //Business code

            _certificateDal.DeleteAll(filter);
            return new SuccessResult(Message.ActivityDeleted);
        }

        public IDataResult<List<Certificate>> GetAll()
        {
            //Business code

            var data = _certificateDal.GetAll();
            return new SuccessDataResult<List<Certificate>>(data, Message.ActivitiesListed);
        }

        public IDataResult<Certificate> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Certificate>(Message.MaintenanceTime);
            }

            var data = _certificateDal.Get(c => c.Id == id);
            return new SuccessDataResult<Certificate>(data);
        }


        public IResult Update(Certificate certificate)
        {
            //Business code

            _certificateDal.Update(certificate);
            return new SuccessResult(Message.ActivityUpdated);
        }
    }
}
