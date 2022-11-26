using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    //Burası bizim bütün metotlarımızın çatısıdır. Business metotlarımız önce şu kurallardan geçecektir.
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //invocation -> business methoda karşılık gelir.
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {
            //Önce bir dener ve try ile çalıştırır, hata alırsa catch'le yakalar.
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            //Ama hata olsun olmasın finally bloğu her zaman çalışır.
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
