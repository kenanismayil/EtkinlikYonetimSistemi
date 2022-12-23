using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICertificateDal : IEntityRepository<Certificate>
    {
        UserCertificateInfo GetCertificates(int activityId, int userId);
        List<UserCertificateInfo> GetCertificatesForUser(int userId);
        UserCertificateInfo GetByCertificateId(int certificateId);

    }
}
