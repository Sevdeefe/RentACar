﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class SuccesDataResult<T> : DataResult<T>
    {
        public SuccesDataResult(T data, string message) : base(data, true, message)
        {
        }
        public SuccesDataResult(T data) : base(data, true)
        {

        }
        public SuccesDataResult(string massage) : base(default, true, massage)
        {

        }
        public SuccesDataResult() : base(default, true)
        {

        }
    }
}
