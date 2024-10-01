using Core.DataAcces;
using Core.DataAcces.EntityFremework;
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
    public interface IRentalDal : IEntityRepository<Rental>
    {
        List<RentalDetailsDto> GetRentalsDetails(Expression<Func<RentalDetailsDto, bool>> filter = null);
    }
}
