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
    public class RegistrationManager : IRegistrationService
    {
        IRegistrationDal _registrationDal;
        public RegistrationManager(IRegistrationDal registrationDal)
        {
            _registrationDal = registrationDal;
        }

        public IResult Add(Registration registration)
        {
            //Business code

            _registrationDal.Add(registration);
            return new SuccessResult(Message.RegistrationAdded);
        }

        public IResult Delete(Registration registration)
        {
            //Business code

            _registrationDal.Delete(registration);
            return new SuccessResult(Message.RegistrationDeleted);
        }

        public IResult DeleteAll(Expression<Func<Registration, bool>> filter)
        {
            //Business code

            _registrationDal.DeleteAll(filter);
            return new SuccessResult(Message.RegistrationDeleted);
        }

        public IDataResult<List<Registration>> GetAll()
        {
            //Business code

            var data = _registrationDal.GetAll();
            return new SuccessDataResult<List<Registration>>(data, Message.ActivitiesListed);
        }

        public IDataResult<Registration> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Registration>(Message.MaintenanceTime);
            }

            var data = _registrationDal.Get(c => c.Id == id);
            return new SuccessDataResult<Registration>(data);
        }


        public IResult Update(Registration registration)
        {
            //Business code

            _registrationDal.Update(registration);
            return new SuccessResult(Message.RegistrationUpdated);
        }
    }
}
