﻿
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger))); //Burası sistemimizdeki tüm metodlarımızı loglamamızı sağlar. Şuanda o altyapıyı hazır olmadığı için comment'li duruyor.
            //classAttributes.Add(new PerformanceAspect(0)); // Reports the running time of all methods to the Debug screen


            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
