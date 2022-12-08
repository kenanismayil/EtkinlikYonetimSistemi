using Business.Abstract;
using Business.Concrete;
using Core.Utilities.FileHelper;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomsServices(this IServiceCollection services)
        {
            //Activity icin IoC Container
            services.AddSingleton<IActivityService, ActivityManager>();
            services.AddSingleton<IActivityDal, EfActivityDal>();

            //ActivityType icin IoC Container
            services.AddSingleton<IActivityTypeService, ActivityTypeManager>();
            services.AddSingleton<IActivityTypeDal, EfActivityTypeDal>();

            //Registration icin IoC Container
            services.AddSingleton<IRegistrationService, RegistrationManager>();
            services.AddSingleton<IRegistrationDal, EfRegistrationDal>();

            //Certificate icin IoC Container
            services.AddSingleton<ICertificateService, CertificateManager>();
            services.AddSingleton<ICertificateDal, EfCertificateDal>();

            //City icin IoC Container
            services.AddSingleton<ICityService, CityManager>();
            services.AddSingleton<ICityDal, EfCityDal>();

            //Country icin IoC Container
            services.AddSingleton<ICountryService, CountryManager>();
            services.AddSingleton<ICountryDal, EfCountryDal>();

            //Comment icin IoC Container
            services.AddSingleton<ICommentService, CommentManager>();
            services.AddSingleton<ICommentDal, EfCommentDal>();

            //User icin IoC Container
            services.AddSingleton<IUserService, UserManager>();
            services.AddSingleton<IUserDal, EfUserDal>();

            //User icin IoC Container
            services.AddSingleton<ILocationService, LocationManager>();
            services.AddSingleton<ILocationDal, EfLocationDal>();

            //ActivityImage icin IoC Container
            services.AddSingleton<IRoleTypeService, RoleTypeManager>();
            services.AddSingleton<IRoleTypeDal, EfRoleTypeDal>();


            //Moderator icin IoC Container
            //services.AddSingleton<IModeratorService, ModeratorManager>();
            //services.AddSingleton<IModeratorDal, EfModeratorDal>();
            //services.AddSingleton<IFileHelper, FileHelperManager>();


            //Auth icin IoC Container
            services.AddSingleton<IAuthService, AuthManager>();

            //JWT icin IoC Container
            services.AddSingleton<ITokenHelper, JwtHelper>();

            return services;
        }
    }
}
