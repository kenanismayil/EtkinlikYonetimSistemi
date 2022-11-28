using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Aşağıdaki iki class da error detaylarını göstermek için kullanılan class'lardır. Fakat ikisini birbirinden ayıran fark vardır.
//Validasyon hatası ErrorDetails'teki property'leri kullanır. Ama ErrorDetails validasyonu kullanamaz.
namespace Core.Extensions
{
    //Sistemsel hatalar için bu class kullanılır.
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        //ErrorDetails'i json formatına çevirir.(Newtonsoft.Json kütüphanesinin aşağıdaki metotunu kullanarak)
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    //Validasyon hataları için bu class kullanılır.
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
