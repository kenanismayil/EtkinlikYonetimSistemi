﻿using Core.Utilities.Results.Abstract;
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
    public interface ICertificateService
    {
        IDataResult<List<Certificate>> GetAll();
        IDataResult<Certificate> Get(int userId, int activityId);
        IDataResult<UserCertificateInfo> GetByCertificateId(int certificateId);
        IDataResult<List<UserCertificateInfo>> GetCertificatesForUser(string token);
        //IDataResult<UserCertificateInfo> GetCertificates(int activityId, string token);
        IResult Add(CertificateForView certificate);
        IResult Delete(int certificateId);
        IResult Update(CertificateForView certificate);

        IResult UpdateCertificateImage(UpdateCertificateImageDto data);
    }
}
