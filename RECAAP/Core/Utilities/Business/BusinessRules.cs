using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        // Run metodu: Parametre olarak gelen IResult nesnelerini değerlendirir
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                // Her bir IResult nesnesinin Success özelliğini kontrol eder
                if (!logic.Success)
                {
                    // İlk başarısız olan logic nesnesini döner
                    return logic;
                }
            }

            // Tüm logics başarılıysa null döner
            return null;
        }
    }
}

