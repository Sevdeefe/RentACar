﻿using Core.Entities.Concrete;
using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccesToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
