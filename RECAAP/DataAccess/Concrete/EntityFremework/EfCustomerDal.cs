






using Core.DataAcces.EntityFremework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFremework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentACarContext>, ICustomerDal
    {
        public List<CustomerDetailsDto> GetCustomerDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var customerDetail = from c in context.Customers
                                     join u in context.Users
                                     on c.UserId equals u.Id
                                     select new CustomerDetailsDto
                                     {
                                         CompanyName = c.CompanyName,
                                         CustomerId = c.CustomerId,
                                         Email = u.Email,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         FindeksScore = c.FindeksScore
                                     };
                return customerDetail.ToList();



            }
        }
    }
}
