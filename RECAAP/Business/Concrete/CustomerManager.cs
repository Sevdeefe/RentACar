using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            var result = BusinessRules.Run(CheckFindeksScoreMax(customer));
            if (result != null)
            {
                return result;
            }
            _customerDal.Add(customer);
            return new SuccessResult(Massages.CustomerAdded);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Massages.CustomerDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccesDataResult<List<Customer>>(_customerDal.GetAll(), Massages.CustomersListed);
        }

        public IDataResult<Customer> GetCustomerById(int id)
        {
            return new SuccesDataResult<Customer>(_customerDal.Get(c => c.CustomerId == c.CustomerId), Massages.CustomerListed);
        }

        public IDataResult<Customer> GetCustomerByUserId(int userId)
        {
            return new SuccesDataResult<Customer>(_customerDal.Get(c => c.UserId == c.UserId));
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Massages.CustomerUpdated);
        }
        public IResult CheckFindeksScoreMax(Customer customer)
        {
            if (customer.FindeksScore > 1900)
            {
                return new ErrorResult(Massages.FindeksScoreMax);
            }

            return new SuccessResult(Massages.FindeksScoreSuccesful);
        }
    }
}