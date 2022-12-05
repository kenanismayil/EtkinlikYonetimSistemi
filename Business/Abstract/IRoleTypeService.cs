using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleTypeService
    {
        IResult Add(RoleType role);
        IResult Update(RoleType role);
        IResult Delete(RoleType role);

        IDataResult<List<RoleType>> GetAll();
        IDataResult<RoleType> GetById(int roleTypeId);

    }
}
