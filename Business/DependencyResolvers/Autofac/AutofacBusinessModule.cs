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
using Business.CCS;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;

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

            //builder.RegisterType<FileLogger>().As<ILogger>().SingleInstance();

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

            //User icin IoC Container
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();


            //ActivityImage icin IoC Container
            builder.RegisterType<ActivityImageManager>().As<IActivityImageService>().SingleInstance();
            builder.RegisterType<EfActivityImageDal>().As<IActivityImageDal>().SingleInstance();

            //Auth icin IoC Container
            builder.RegisterType<AuthManager>().As<IAuthService>();

            //JWT icin IoC Container
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //builder.RegisterType<TurkishMessage>().As<IMessage>().SingleInstance();

            //.net mimarisinde de autofac vardır ama Autofac, aynı zamanda bize Interceptor özelliği verir. 

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();   //Uygulamayı çalıştırır.

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()   //Çalışan uygulama içerisinde implemente edilmiş interface'leri bulur ve onlar için AspectInterceptorSelector()'u çağır.
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

            //Kısacası, Autofac yukarıda register yaptığımız bütün sınıflarımız için önce AspectInterceptorSelector()'u çalıştırır. Bu sınıfların bir aspect'i var mı ona bakıyor.

        }
    }
}
