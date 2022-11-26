using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    //ValidationAspect -> bu bir attribute'dur ve MethodInterception'ten miras almaktadır. Bu attribe, bir validatorType alıyor.
    //Attributelar typeof ile çalışır. İş metotunun üstüne ValidationAspect ile çalışacağımız validatorun tipini göndeririz.
    public class ValidationAspect : MethodInterception  //Aspect 
    {
        //Attribute'larda Type ile yapılır.
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //DEFENSİVE CODİNG -> savunma odaklı kodlama yapılmıştır.
            //Eğer gönderilen validator tipi bir validator tipi değilse, hata fırlatır.
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değildir.");
            }

            _validatorType = validatorType;
        }

        //MethodInterception metotlarından biri olan OnBefore'un üzerine override yaparak business metotunun önünde doğrulama gerçekleşir.
        protected override void OnBefore(IInvocation invocation)
        {
            //Reflection, çalışma anında bir class'ın instance'nı oluşturmak istersek kullanılır. Ör; biz herhangi bir class'ı newlediğimizde, ama biz bunu çalışma anında yapmak istersek Reflection kullanırız.
            //(IValidator)Activator.CreateInstance(_validatorType); -> Herhangi bir validator tipine sahip class'ın newlenmesi yapılmaktadır. örneğin: Çalışma anında ActivityValidator'un örneğini oluşturur ve gidip onun çalışacağı base tipi, yani AbstractValidator'u bulur.
            //GetGenericArguments()[0] -> Onun generic argumanlarından ilkini, örneğin: Activity'i bulur. invocation -> metot demektir.
            //Sonrasında metotun parametrelerini bulur. Yani ilgili metotun, örneğin: iş katmanındaki Add metotunun parametresini (activity)'i bulur.
            //En sonda foreach ile bu parametreler üzerinde gezerek bütün parametreleri doğrulama işlemini yapar.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);  //Reflection
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);   //Merkezi bir noktaya aldım.
            }
        }
    }
}
