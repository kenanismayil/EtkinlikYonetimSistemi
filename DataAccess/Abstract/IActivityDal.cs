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
    //Veri Erisim Islemini yapacak IActivityDal interface'i olusturdum ve IEntityRepository'e bagladim.
    public interface IActivityDal : IEntityRepository<Activity>
    {
        List<ActivityDetailDto> GetActivityDetails();
        //List<User> GetParticipiantstoActivity(int activityId);
    }
}
