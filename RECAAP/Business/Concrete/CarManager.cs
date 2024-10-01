using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Core.Utilities.Business;
using System.Runtime.Remoting.Messaging;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        ICarImageService _carImageService;
        //Farkli ortamlara geciste kolaylik saglamasi icin burada bir referans tutucu blogu olusturuyoruz.              
        //Bu bugun Sql yarin MySql baska bir gunde PostgreSql referansi tutabilir.

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;

        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            //Business codes
            _carDal.Add(car);
            return new SuccessResult(Massages.CarAdded);
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            Add(car);
            if (car.DailyPrice < 2000)
            {
                throw new Exception("");
            }

            Add(car);

            return null;
        }

        public IResult Delete(Car car)
        {
            var rulesResult = BusinessRules.Run(CheckIfCarIdExist(car.CarId));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var deletedCar = _carDal.Get(c => c.CarId == car.CarId);
            _carDal.Delete(deletedCar);
            return new SuccessResult(Massages.CarDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 7)
            {
                return new ErrorDataResult<List<Car>>(Massages.MaintenanceTime);
            }
            return new SuccesDataResult<List<Car>>(_carDal.GetAll(), Massages.CarsListed);
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Car> GetById(int carId)
        {
            return new SuccesDataResult<Car>(_carDal.Get(c => c.CarId == carId), Massages.CarListed);
        }

        public IDataResult<List<CarDetailsDto>> GetCarByBrandAndColor(int brandId, int colorId)
        {
            return new SuccesDataResult<List<CarDetailsDto>>(_carDal.GetCarByBrandAndColor(brandId, colorId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            /*if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }*/
            return new SuccesDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails(), Massages.CarDetailsListed);
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetailsId(int carId)
        {
            List<CarDetailsDto> carDetails = _carDal.GetCarDetails(c => c.CarId == carId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailsDto>>("");
            }
            else
            {
                return new SuccesDataResult<List<CarDetailsDto>>(carDetails, "");
            }
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccesDataResult<List<Car>>(_carDal.GetAll(b => b.BrandId == brandId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarsByBrandIdWithDetails(int brandId)
        {
            List<CarDetailsDto> carDetails = _carDal.GetCarDetails(c => c.BrandId == brandId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailsDto>>("");
            }
            else
            {
                return new SuccesDataResult<List<CarDetailsDto>>(carDetails, "");
            }
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccesDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailsDto>> GetCarsByColorIdWithDetails(int colorId)
        {
            List<CarDetailsDto> carDetails = _carDal.GetCarDetails(c => c.ColorId == colorId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailsDto>>("");
            }
            else
            {
                return new SuccesDataResult<List<CarDetailsDto>>(carDetails, "");
            }
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Massages.CarUpdated);
        }
        private IResult CheckIfCarIdExist(int carId)
        {
            var result = _carDal.GetAll(c => c.CarId == carId).Any();
            if (!result)
            {
                return new ErrorResult(Massages.CarNotExist);
            }
            return new SuccessResult();
        }
    }
}









