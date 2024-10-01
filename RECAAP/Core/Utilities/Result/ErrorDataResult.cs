using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string massage) : base(data, true, massage)
        {
        }
        public ErrorDataResult(T data) : base(data, true)
        {

        }
        public ErrorDataResult(string massage) : base(default, true, massage)
        {

        }
        public ErrorDataResult() : base(default, true)
        {

        }
    }
}
