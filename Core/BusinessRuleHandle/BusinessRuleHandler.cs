using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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
        /// <param name="rules"></param>
        /// <returns>Hatalı kullanımlarla ilgili bilgi.</returns>
        
        public static IResult CheckTheRules(params IResult[] rules)
        {
            int count = 0;
            string message = "";
            foreach (var rule in rules)
            {
                if (!rule.Success)
                {
                    message += rule.Message;
                    message += "/n";
                    count++;
                }
            }

            if (count != 0)
            {
                return new ErrorResult(message);
            }

            return new SuccessResult(message);
        }
    }
}
