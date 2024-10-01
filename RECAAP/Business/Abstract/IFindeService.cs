using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFindeksService
    {
        IDataResult<List<Findeks>> GetAll();
        IDataResult<List<FindeksDetailsDto>> GetFindeksDetail();
        IDataResult<List<FindeksDetailsDto>> GetFindeksDetailByUserId(int userId);
        IDataResult<List<FindeksDetailsDto>> GetFindeksDetailByFindeksId(int findeksId);
        IResult Add(Findeks findeks);
        IResult Delete(Findeks findeks);
        IResult Update(Findeks findeks);

    }
}
