using Business.Abstract;
using Business.BusinessAspcets.Autofac;
using Business.CCS;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.BusinessRuleHandle;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.ExceptionHandle;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ActivityManager : IActivityService
    {
        //Bir entity manager, kendi Dal'ı hariç başka bir Dal'ı enjekte edemez!
        IActivityDal _activityDal;
        IActivityTypeService _activityTypeService;  //aktivite tipi tablosunu ilgilendiren bir kural olduğu için bu servisi enjekte deriz, Dal'ı değil!
        IUserService _userService;
        IRegistrationService _registrationService;
        public ActivityManager(IActivityDal activityDal, IActivityTypeService activityTypeService, IUserService userService,
            IRegistrationService registrationService)
        {
            _activityDal = activityDal;
            _activityTypeService = activityTypeService;
            _userService = userService;
            _registrationService = registrationService;
        }

        //Validation -> Add metotunu ActivityValidator'daki kurallara göre doğrulanması
        //Claim -> admin, activity.add,super_admin.  activity.add -> operasyon bazlı yapılarda kullanılır. 
        [ValidationAspect(typeof(ActivityValidator))]
        [CacheRemoveAspect("IActivityService.Get")]
        public IResult Add(ActivityCreatingByAdmin activity)
        {
            //Business codes -> Polymorphism yaptım.
            var activityData = new Activity()
            {
                UserId = activity.UserId,
                ActivityTypeId = activity.ActivityTypeId,
                ActivityName = activity.ActivityName,
                LocationId = activity.LocationId,
                Participiant = activity.Participiant,
                AppDeadLine = activity.AppDeadLine,
                ActivityDate = activity.ActivityDate
            };

            IResult result = BusinessRules.Run(CheckIfActivityCountOfTypeCorrect(activityData.ActivityTypeId), 
                CheckIfActivityNameExists(activityData.ActivityName), CheckIfActivityTypeLimitExceded());

            //result -> kurala uymayan
            //result null değilse, yani kurala uymayan bir durum oluşmuşsa, o zaman result kendisi döner. ErrorResult dönecektir.
            if (result != null)
            {
                return result;
            }

            //Kurala uymayan bir durum oluşmamışsa aktivite veritabanına başarılı şekilde eklenir.
            activityData.CreatedTime = DateTime.Now;
            _activityDal.Add(activityData);
            return new SuccessResult(TurkishMessage.ActivityAdded);
        }


        [CacheRemoveAspect("IActivityService.Get")]
        public IResult Delete(string activityId)
        {
            //Business codes
            var activity = _activityDal.Get(a => a.Id.ToString() == activityId);

            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityDal.Delete(activity);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityDeleted);
        }

        //Validation
        [ValidationAspect(typeof(ActivityValidator))]
        [CacheRemoveAspect("IActivityService.Get")]
        public IResult Update(ActivityCreatingByAdmin activity)
        {
            //Business code
            var activityData = new Activity()
            {
                UserId = activity.UserId,
                ActivityTypeId = activity.ActivityTypeId,
                ActivityName = activity.ActivityName,
                LocationId = activity.LocationId,
                Participiant = activity.Participiant,
                AppDeadLine = activity.AppDeadLine,
                ActivityDate = activity.ActivityDate
            };

            IResult result = BusinessRules.Run(CheckIfActivityCountOfTypeCorrect(activityData.ActivityTypeId),
                CheckIfActivityNameExists(activityData.ActivityName), CheckIfActivityTypeLimitExceded());

            //result -> kurala uymayan
            //result null değilse, yani kurala uymayan bir durum oluşmuşsa, o zaman result kendisi döner. ErrorResult dönecektir.
            if (result != null)
            {
                return result;
            }

            //Kurala uymayan bir durum oluşmamışsa aktivite veritabanına başarılı şekilde eklenir.
            activityData.CreatedTime = DateTime.Now;
            _activityDal.Update(activityData);
            return new SuccessResult(TurkishMessage.ActivityUpdated);
        }

        [CacheRemoveAspect("IActivityService.Get")]
        public IResult DeleteAll(Expression<Func<Activity, bool>> filter)
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithNoReturn(() =>
            {
                _activityDal.DeleteAll(filter);
            });
            if (!result)
            {
                return new ErrorResult(TurkishMessage.ErrorMessage);
            }

            return new SuccessResult(TurkishMessage.ActivityDeleted);
        }

        [CacheAspect]
        public IDataResult<List<ActivityDetailDto>> GetActivityDetails()
        {
            //Business code


            //Central Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<ActivityDetailDto>>(() =>
            {
                return _activityDal.GetActivityDetails();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<ActivityDetailDto>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<ActivityDetailDto>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        //Cachlemek istediğimiz bir datayı bellekte Key,Value olarak tutuyoruz.
        [CacheAspect]
        public IDataResult<List<Activity>> GetAll()
        {
            //Business code


            //Cetnral Management System
            var result = ExceptionHandler.HandleWithReturnNoParameter<List<Activity>>(() =>
            {
                return _activityDal.GetAll();
            });
            if (!result.Success)
            {
                return new ErrorDataResult<List<Activity>>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<List<Activity>>(result.Data, TurkishMessage.ActivitiesListed);
        }

        [CacheAspect]
        public IDataResult<Activity> GetById(int activityId)
        {
            //Business code
            var result = ExceptionHandler.HandleWithReturn<int, Activity>((x) =>
            {
                return _activityDal.Get(a => a.Id == x);
            }, activityId);
            if (!result.Success)
            {
                return new ErrorDataResult<Activity>(TurkishMessage.ErrorMessage);
            }

            return new SuccessDataResult<Activity>(result.Data, TurkishMessage.SuccessMessage);
        }

        //public IDataResult<Activity> GetCurrentAttendiesCount(int activityId)
        //{
        //    //Activity tablosuna maksimum katılımcı sayısı da eklenmesi gerekiyor.

        //    var activityData = _activityDal.GetAll(a => a.Id == activityId).Count;



        //    var registeredToActivity = _registrationService.GetById(activityId);

        //    int currentUserCount, maxUserCount;
        //    currentUserCount = registeredToActivity.Data.User.Id;

        //    if (activityData.)
        //    {

        //    }


        //    return new SuccessDataResult<Activity>(TurkishMessage.SuccessMessage);
        //}



        //İş kuralı parçacığı olduğu için ve sadece bu manager altında kullanacağım iş kuralı parçacığını buraya yazıyorum. Ekleme ve güncellemede kod tekrarlılığını önlemiş oldum.
        private IResult CheckIfActivityCountOfTypeCorrect(int? activityTypeId)
        {
            //Aktivite eklemek istediğimizde eklemek istediğimiz aktivitenin tipinde maksimum 10 aktivite olabilir.
            //Yani aynı aktivite tipinde en fazla 10 tane aktivite olabilir.
            //_activityDal.GetAll(a => a.ActivityTypeId == activityTypeId).Count -> Bizim için arka planda bir Linq Query oluşturuyor ve bu query'yi veritabanına yolluyor.
            //Select count(*) from Activities where activityTypeId=1 -> bu query arka planda bu sorguyu çalıştırır.
            var result = _activityDal.GetAll(a => a.ActivityTypeId == activityTypeId).Count;
            if (result >= 100)
            {
                return new ErrorResult(TurkishMessage.ActivityCountOfTypeError);
            }
            return new SuccessResult();
        }


        private IResult CheckIfActivityNameExists(string activityName)
        {
            //Aynı isimde aktivite eklenemez
            //Any() -> _activityDal.GetAll(a => a.ActivityName == activityName)'e uyan kayıt var mı anlamına gelen metottur.
            var result = _activityDal.GetAll(a => a.ActivityName == activityName).Any();
            if (result)
            {
                return new ErrorResult(TurkishMessage.ActivityNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfActivityTypeLimitExceded()
        {
            //Eğer toplam mevcut aktivite tipi sayısı 100'ü geçmişse sisteme yeni aktivite eklenemez.
            var result = _activityTypeService.GetAll();
            if (result.Data.Count > 100)
            {
                return new ErrorResult(TurkishMessage.ActivityTypeLimitExceded); 
            }
            return new SuccessResult();
        }
    }
}
