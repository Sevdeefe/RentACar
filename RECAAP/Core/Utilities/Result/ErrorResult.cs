﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class ErrorResult : Result
    {
        public ErrorResult( string massage) : base(false, massage)
        {
        }
        public ErrorResult() : base(false)
        {

        }
    }
}
