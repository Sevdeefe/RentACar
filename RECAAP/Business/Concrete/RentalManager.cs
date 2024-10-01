using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            if (rental.ReturnDate == null)
            {
                return new ErrorResult(Massages.RentalCarError);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Massages.RentalAdded);

        }

        public IDataResult<Rental> CheckRentalCarId(int carId)
        {
            var rental = _rentalDal.Get(r => r.CarId == carId && r.ReturnDate == null);
            if (rental != null)
            {
                return new ErrorDataResult<Rental>("Araç müsait değil, kiralanmış durumda");
            }
            return new SuccesDataResult<Rental>("Araç müsait, kiralanabilir durumda");
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Massages.RentalDeleted);
        }
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccesDataResult<List<Rental>>(_rentalDal.GetAll(), Massages.RentalsListed);
        }

        public IDataResult<List<RentalDetailsDto>> GetRentalsDetails()
        {
            return new SuccesDataResult<List<RentalDetailsDto>>(_rentalDal.GetRentalsDetails(), Massages.RentalsListed);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Massages.RentalUpdated);
        }

        public IResult CheckRental(Rental entity)
        {
            var result = BusinessRules.Run(
                CheckIfThisCarIsAlreadyRentedInSelectedDateRange(entity),
                CheckIfReturnDateIsBeforeRentDate(entity.ReturnDate, entity.RentDate)
                );
            if (result != null)
            {
                return result;
            }


            return new SuccessResult("Ödeme Sayfasına Yönlendiriliyorsunuz.");
        }

        private IResult CheckIfThisCarIsAlreadyRentedInSelectedDateRange(Rental entity)
        {
            var result = _rentalDal.Get(r =>
            r.CarId == entity.CarId
            && (r.RentDate.Date == entity.RentDate.Date
            || (r.RentDate.Date < entity.RentDate.Date
            && (r.ReturnDate == null
            || ((DateTime)r.ReturnDate).Date > entity.RentDate.Date))));

            if (result != null)
            {
                return new ErrorResult(Massages.ThisCarIsAlreadyRentedInSelectedDateRange);
            }
            return new SuccessResult();

        }
        private IResult CheckIfThisCarHasBeenReturned(Rental entity)
        {
            var result = _rentalDal.Get(r => r.CarId == entity.CarId && r.ReturnDate == null);

            if (result != null)
            {
                if (entity.ReturnDate == null || entity.ReturnDate > result.RentDate)
                {
                    return new ErrorResult(Massages.ThisCarIsAlreadyRentedInSelectedDateRange);
                }
            }
            return new SuccessResult();
        }
        private IResult CheckIfReturnDateIsBeforeRentDate(DateTime? returnDate, DateTime rentDate)
        {
            if (returnDate != null)
                if (returnDate < rentDate)
                {
                    return new ErrorResult(Massages.ThisCarIsAlreadyRentedInSelectedDateRange);
                }
            return new SuccessResult();
        }


    }
}