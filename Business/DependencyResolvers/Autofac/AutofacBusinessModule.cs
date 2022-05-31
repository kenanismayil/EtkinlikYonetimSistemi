using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    //IoC Container --> Startup yerine referans alicilari ve verici burda yazilir.
    public class AutofacBusinessModule : Module
    {
        //Uygulama ayaga kalktigi zaman, yani hayata gectigi zaman bu method calisir.
        protected override void Load(ContainerBuilder builder)
        {
            //Bir projeyi farkli farkli musterilere satarken, fakat musteriler farkli veritabanlarini kullanabilirler.
            //Bu yuzden bu IoC Container'ler yapilmalidir. Bu da Autofac yapilandirmasi ile mumkundur.

            //Projeye farkli api ve ya servis ekledigimiz zaman bizim tum configlerimiz sadece ilgili API'de kaliyor
            //Buna gore Autofac yapilandirmasi daha kolay ve anlasilir sekilde genel bir yapi oldugundan ve herhangi API'ler
            //ekledigimizde sorun olusmamasi icin Business katmaninda DependencyResolver klasorunde olusturulur.

            //Startup'daki services.AddSingleton<IProductService, ActivityManager>(); ' a karsilik gelir.

            //Activity icin IoC Container
            builder.RegisterType<ActivityManager>().As<IActivityService>().SingleInstance();
            builder.RegisterType<EfActivityDal>().As<IActivityDal>().SingleInstance();

            //ActivityType icin IoC Container
            builder.RegisterType<ActivityTypeManager>().As<IActivityTypeService>().SingleInstance();
            builder.RegisterType<EfActivityTypeDal>().As<IActivityTypeDal>().SingleInstance();

            //Registration icin IoC Container
            builder.RegisterType<RegistrationManager>().As<IRegistrationService>().SingleInstance();
            builder.RegisterType<EfRegistrationDal>().As<IRegistrationDal>().SingleInstance();

            //Certificate icin IoC Container
            builder.RegisterType<CertificateManager>().As<ICertificateService>().SingleInstance();
            builder.RegisterType<EfCertificateDal>().As<ICertificateDal>().SingleInstance();

            //City icin IoC Container
            builder.RegisterType<CityManager>().As<ICityService>().SingleInstance();
            builder.RegisterType<EfCityDal>().As<ICityDal>().SingleInstance();

            //Country icin IoC Container
            builder.RegisterType<CountryManager>().As<ICountryService>().SingleInstance();
            builder.RegisterType<EfCountryDal>().As<ICountryDal>().SingleInstance();

            //Comment icin IoC Container
            builder.RegisterType<CommentManager>().As<ICommentService>().SingleInstance();
            builder.RegisterType<EfCommentDal>().As<ICommentDal>().SingleInstance();

            //Moderator icin IoC Container
            builder.RegisterType<ModeratorManager>().As<IModeratorService>().SingleInstance();
            builder.RegisterType<EfModeratorDal>().As<IModeratorDal>().SingleInstance();

            //User icin IoC Container
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            //builder.RegisterType<TurkishMessage>().As<IMessage>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
