﻿using Core.DataAcces;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IFindeksDal : IEntityRepository<Findeks>
    {
        List<FindeksDetailsDto> GetFindeksDetail(Expression<Func<FindeksDetailsDto, bool>> filter = null);
        void Update(Findeks findeks);
    }
}
