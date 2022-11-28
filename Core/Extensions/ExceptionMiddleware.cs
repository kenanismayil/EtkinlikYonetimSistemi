using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);      //apimizde bütün metotları çalıştırır.(InvokeAsync metotu ile)
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);    //herhangi bir istek sonucu hata varsa bu fonksiyonu çalıştır.(mevcut contect için o exception'ı yolladım)
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";      //tarayıcımıza json formatta content gönderiyoruz.
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;  //statuscode->gönderdiğimiz hata kodudur.

            string message = "Internal Server Error";       //statü kodu burada 500'dür.(sistemkle ilgili hata)
            IEnumerable<ValidationFailure> errors;      //hataların tümünü almak için IEnumerable kolleksiyonu tanımladım.
            if (e.GetType() == typeof(ValidationException))         //hatanın tipi ValidationException ise;
            {
                message = e.Message;                                //mesajı hatanın mesajıyla değiştir.
                errors = ((ValidationException)e).Errors;       //hatanın tipini vererek yukarıdaki listeye atadım.
                httpContext.Response.StatusCode = 400;          //statü kodunu verdim. 400 bad requesttir.

                //Validation hatalarını döndürmek için bir ErrorDetails nesnesi oluşturup içerisine statü kodu, mesaj ve errorlarımı yazarak döndürdüm.
                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,                           //statü kodunu verdim. 400 bad requesttir.
                    Message = message,
                    Errors = errors
                }.ToString());
            }

            //Atılan istek hata verirse mesaj ve statüs kodunu döndürür.
            //Buradaki ErrorDetails sistemsel bir hata döndürür. Ör: veritabanımız çalışmıyor.
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());      //ToString diyerek json formata çevirdim.
        }
    }
}
