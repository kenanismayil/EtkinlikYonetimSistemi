using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    //Veri Erisim Islemini yapacak ILocationDal interface'i olusturdum ve IEntityRepository'le ilgili entity'i bagladim.
    public interface ILocationDal : IEntityRepository<Location>
    {

    }
}
