using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FindeksManager : IFindeksService
    {
        private IFindeksDal _findeksDal;

        public FindeksManager(IFindeksDal findeksDal)
        {
            _findeksDal = findeksDal;
        }

        public IDataResult<List<Findeks>> GetAll()
        {
            return new SuccesDataResult<List<Findeks>>(_findeksDal.GetAll());
        }

        public IDataResult<List<FindeksDetailsDto>> GetFindeksDetail()
        {
            return new SuccesDataResult<List<FindeksDetailsDto>>(_findeksDal.GetFindeksDetail());
        }

        public IDataResult<List<FindeksDetailsDto>> GetFindeksDetailByUserId(int userId)
        {
            List<FindeksDetailsDto> findeksDetail = _findeksDal.GetFindeksDetail(x => x.UserId == userId);
            return new SuccesDataResult<List<FindeksDetailsDto>>(findeksDetail);


        }

        public IDataResult<List<FindeksDetailsDto>> GetFindeksDetailByFindeksId(int findeksId)
        {
            List<FindeksDetailsDto> findeksDetail = _findeksDal.GetFindeksDetail(x => x.FindeksId == findeksId);
            return new SuccesDataResult<List<FindeksDetailsDto>>(findeksDetail);
        }

        public IResult Add(Findeks findeks)
        {
            _findeksDal.Add(findeks);
            return new SuccessResult(Massages.FindeksAdded);
        }

        public IResult Delete(Findeks findeks)
        {
            _findeksDal.Delete(findeks);
            return new SuccessResult(Massages.FindeksDeleted);
        }

        public IResult Update(Findeks findeks)
        {
            _findeksDal.Update(findeks);
            return new SuccessResult(Massages.FindeksUpdated);
        }
    }
}