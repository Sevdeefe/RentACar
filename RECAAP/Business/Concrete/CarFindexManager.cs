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
    public class CarFindeksManager : ICarFindeksService
    {
        private ICarFindeksDal _findeksDal;

        public CarFindeksManager(ICarFindeksDal findeksDal)
        {
            _findeksDal = findeksDal;
        }

        public IDataResult<List<CarFindeks>> GetAll()
        {
            return new SuccesDataResult<List<CarFindeks>>(_findeksDal.GetAll());
        }

        public IDataResult<List<CarFindeksDetailDto>> GetCarFindeksDetail()
        {
            return new SuccesDataResult<List<CarFindeksDetailDto>>(_findeksDal.GetFindeksDetail());

        }

        public IDataResult<List<CarFindeksDetailDto>> GetCarFindeksDetailByCarId(int carId)
        {
            List<CarFindeksDetailDto> carFindeksDetail = _findeksDal.GetFindeksDetail(x => x.CarId == carId);
            return new SuccesDataResult<List<CarFindeksDetailDto>>(carFindeksDetail);
        }

        public IDataResult<List<CarFindeksDetailDto>> GetCarFindeksDetailById(int findeksId)
        {

            List<CarFindeksDetailDto> carFindeksDetail = _findeksDal.GetFindeksDetail(x => x.FindeksId == findeksId);
            return new SuccesDataResult<List<CarFindeksDetailDto>>(carFindeksDetail);
        }

        public IResult Add(CarFindeks carFindeks)
        {
            _findeksDal.Add(carFindeks);
            return new SuccessResult(Massages.CarFindeksAdded);
        }

        public IResult Delete(CarFindeks carFindeks)
        {
            _findeksDal.Delete(carFindeks);
            return new SuccessResult(Massages.CarFindeksDeleted);
        }

        public IResult Update(CarFindeks carFindeks)
        {
            _findeksDal.Update(carFindeks);
            return new SuccessResult(Massages.CarFindeksUpdated);
        }
    }
}