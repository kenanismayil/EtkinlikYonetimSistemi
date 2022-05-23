using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BusinessRuleHandle
{
    public class BusinessRuleHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rules">İş sınıfındaki tüm iş kurallarını denetleyen fonksiyonlar</param>
        /// <returns>Hatalı kullanımlarla ilgili bilgi.</returns>
        
        public static List<IResult> CheckTheRules(params IResult[] rules)
        {
            var listOfErrors = new List<IResult>();
            foreach (var rule in rules)
            {
                if (!rule.Success)
                {
                    listOfErrors.Add(rule);
                }
            }
            return listOfErrors;
        }
    }
}
