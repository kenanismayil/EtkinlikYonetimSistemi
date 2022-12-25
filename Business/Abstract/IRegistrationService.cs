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
    public interface IRegistrationService
    {
        IDataResult<List<Registration>> GetAll();
        IDataResult<UserRegisterInfo> GetRegisterInfo(int activityId, string token);
        IDataResult<List<Registration>> GetRegistersInfoByUserId(string token);
        IDataResult<List<RegistrationForTickets>> GetRegistersInfoByUserIdForTickets(string token);
        IDataResult<Registration> GetRegisterInfoByUserAndActivityId(int userId, int activityId);
        IDataResult<List<UserRegisteredEventsInfo>> GetRegisteredEvents(string token);
        IDataResult<UserInfoForBarcodeReaderPerson> GetUserByPnrNo(string pnrNo);
        IDataResult<UserInfoForBarcodeReaderPerson> UpdateUserStatusOnEventArea(string pnrNo);
        IResult Add(string token, int activityId);
        IResult Delete(RegisterForActivity registration);
        //IResult DeleteAll(Expression<Func<Registration, bool>> filter);
        IResult Update(RegisterForActivity registration);
    }
}
