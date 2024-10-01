using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception //TransactionScope  Bu sınıf, birden fazla veritabanı işlemi veya diğer işlemleri bir
                                                             //transaction içine alarak, bu işlemlerin birlikte ya tamamıyla başarılı olmasını ya da
                                                             //hata durumunda geri alınmasını sağlar.
    {
        public override void Intercept(IInvocation invocation) // TransactionScope kullanarak işlemi başlatıyoruz
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
