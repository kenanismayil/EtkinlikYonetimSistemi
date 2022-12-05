using Business.Abstract;
using Business.Constants.Messages;
using Core.Entities.Concrete;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleTypeManager : IRoleTypeService
    {
        IRoleTypeDal _roleTypeDal;

        public RoleTypeManager(IRoleTypeDal roleTypeDal)
        {
            _roleTypeDal = roleTypeDal;
        }


        public IResult Add(RoleType role)
        {
            //Business Codes


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _roleTypeDal.Add(role);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }
            return new SuccessResult(TurkishMessage.RoleTypeAdded);
        }

        public IResult Delete(RoleType role)
        {
            //Business Codes


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _roleTypeDal.Delete(role);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }
            return new SuccessResult(TurkishMessage.RoleTypeDeleted);
        }

        public IResult Update(RoleType role)
        {
            //Business Codes


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _roleTypeDal.Update(role);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }
            return new SuccessResult(TurkishMessage.RoleTypeUpdated);
        }


        public IDataResult<List<RoleType>> GetAll()
        {
            //Business code


            //Cetnral Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<RoleType>>(() =>
            {
                return _roleTypeDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<RoleType>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<RoleType>>(result.Data, TurkishMessage.RoleTypeListed);
        }

        public IDataResult<RoleType> GetById(int roleTypeId)
        {
            var result = _roleTypeDal.Get(r => r.Id == roleTypeId);
            return new SuccessDataResult<RoleType>(result, TurkishMessage.SuccessMessage);
        }


    }
}
