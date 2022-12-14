using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Extensions;
using Core.Extensions;
using Core.Utilities.DependencyResolvers.Modules.Abstract;
using Core.Utilities.DependencyResolvers.Modules.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Extensions classina ait AddCustomsServices methodunu services'le Startup'da ConfigureService icinden cagirdim.
            services.AddCustomsServices();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });


            services.AddAuthorization(options =>
            {
                // Define the policy here
                options.AddPolicy("Default", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());

                options.AddPolicy("super_admin,admin,user", new AuthorizationPolicyBuilder()
                    .RequireRole("super_admin, admin, user")
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            //services.AddAuthorization();

            //Servisleri autofac'in haberdar olacağı şekilde ayarladım. Daha profosyonel(modul) bir yapı haline getirip ekledim.
            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });

            
            services.AddSwaggerGen(sw =>
                     sw.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                     {
                        Title = "dot6JWTAuthentication",
                        Version = "1.0",
                     }));

            services.AddSwaggerGen(s =>
                    s.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer"
                    }));

            services.AddSwaggerGen(w => w.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                   {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                    new string[]{}
            }
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Aşağıdaki Middleware'lerimizdir. Bunlar hazır middleware'lerdir. Araya kendi middleware'lerimizi de ekleyebiliriz.
        //Bunun için IApplicationBuilder'i kullanarak middleware yazarız.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            //yaşam döngüsü içerisine hata yakalamak için kullandığım middleware'i ekledim
            //Gördüğümüz her yere try catch kodları yazmak yerine API kurallarına uygun olarak try catch'in içerisine aldık.
            app.ConfigureCustomExceptionMiddleware(); 

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        //  dotnet ef database update -c ActivityContext -p .\DataAccess.csproj -s ..\WebAPI\WebAPI.csproj
    }
}
