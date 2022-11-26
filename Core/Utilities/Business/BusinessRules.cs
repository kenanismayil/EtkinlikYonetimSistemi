using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    //İş kurallarını parametre ile Business'e gönderilerek İş motorunun geliştirilmesi
    public class BusinessRules
    {
        //logics -> iş kuralları, params keyword ile Run metotu içerisine istediğimiz kadar IResult türünde parametre gönderebilirim. 
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;       //Sonuç olarak burada parametre ile gönderdiğimiz iş kuralarrından başarısız olanı Business'e haberdar ediyorum, kuralın kendisini ilgili iş metotuna gönderdim. Yani ErrorResult döndürecektir. 
                }
            }

            return null;    //logic başarılı ise hiçbirşey döndürmesine gerek yoktur.
        }
    }
}
