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
    public class CountryManager : ICountryService 
    {
        ICountryDal _countryDal;
        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }


        public IResult Add(Country country)
        {
            //Business code

            _countryDal.Add(country);
            return new SuccessResult(Message.CountryAdded);
        }

        public IResult Delete(Country country)
        {
            //Business code

            _countryDal.Delete(country);
            return new SuccessResult(Message.CountryDeleted);
        }

        public IResult DeleteAll(Expression<Func<Country, bool>> filter)
        {
            //Business code

            _countryDal.DeleteAll(filter);
            return new SuccessResult(Message.CountryDeleted);
        }

        public IDataResult<List<Country>> GetAll()
        {
            //Business code

            var data = _countryDal.GetAll();
            return new SuccessDataResult<List<Country>>(data, Message.CountriesListed);
        }

        public IDataResult<Country> GetById(int id)
        {
            //Business code

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Country>(Message.MaintenanceTime);
            }

            var data = _countryDal.Get(c => c.Id == id);
            return new SuccessDataResult<Country>(data);
        }


        public IResult Update(Country country)
        {
            //Business code

            _countryDal.Update(country);
            return new SuccessResult(Message.CountryUpdated);
        }
    }
}
