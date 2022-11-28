using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        //Startup'ta Middleware'ler olan kısımdaki yaşam döngüsünün içerisine hata yakalamayı da ekle diyorum.
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();       //hata yakalama için ExceptionMiddleware'ni çalıştır dedim.
        }
    }
}
