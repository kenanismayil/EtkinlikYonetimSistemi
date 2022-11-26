using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IActivityImageService
    {
        IDataResult<List<ActivityImage>> GetAll();
        IDataResult<ActivityImage> GetImageByActivityId(int activityId);
        IResult Add(IFormFile formFile, ActivityImage activityImage, string rootPath);
        IResult Delete(ActivityImage activityImage, string rootPath);
        IResult Update(IFormFile formFile, ActivityImage activityImage, string rootPath);
    }
}
