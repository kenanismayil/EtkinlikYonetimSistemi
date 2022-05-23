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
    public class CityManager : ICityService
    {
        ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }


        public IResult Add(City city)
        {
            //Business code

            _cityDal.Add(city);
            return new SuccessResult(Message.CityAdded);
        }

        public IResult Delete(City city)
        {
            //Business code

            _cityDal.Delete(city);
            return new SuccessResult(Message.CityDeleted);
        }

        public IResult DeleteAll(Expression<Func<City, bool>> filter)
        {
            //Business code

            _cityDal.DeleteAll(filter);
            return new SuccessResult(Message.CityDeleted);
        }

        public IDataResult<List<City>> GetAll()
        {
            //Business code

            var data = _cityDal.GetAll();
            return new SuccessDataResult<List<City>>(data, Message.ActivitiesListed);
        }

        public IDataResult<City> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<City>(Message.MaintenanceTime);
            }

            var data = _cityDal.Get(c => c.Id == id);
            return new SuccessDataResult<City>(data);
        }


        public IResult Update(City city)
        {
            //Business code

            _cityDal.Update(city);
            return new SuccessResult(Message.CityUpdated);
        }
    }
}
