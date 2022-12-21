using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRegistrationDal : IEntityRepository<Registration>
    {
        List<UserRegisteredEventsInfo> GetRegisteredEvents(int userId);
        UserInfoForBarcodeReaderPerson GetUserByPnrNo(string pnrNo);
        UserInfoForBarcodeReaderPerson UpdateUserStatusOnEventArea(string pnrNo);
        

    }
}
