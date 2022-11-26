using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    // Bu tip tool'lar genellikle static olarak yapılır. Böylece, tek bir instance(örnek) oluşturulur ve uygulama belleği, bellek boyunca sadece onu kullanır.
    public static class ValidationTool
    {
        //Static bir sınıfın metotları da static olması gerekir!
        public static void Validate(IValidator validator, object entity) 
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
 