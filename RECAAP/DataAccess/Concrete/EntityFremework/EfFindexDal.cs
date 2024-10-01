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
    public class EfFindeksDal : EfEntityRepositoryBase<Findeks, RentACarContext>, IFindeksDal
    {
        public List<FindeksDetailsDto> GetFindeksDetail(Expression<Func<FindeksDetailsDto, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from f in context.Findeks
                             join u in context.Users on f.UserId equals u.Id
                             select new FindeksDetailsDto()
                             {
                                 FindeksId = f.Id,
                                 UserId = u.Id,
                                 UserName = u.FirstName + " " + u.LastName,
                                 FindeksScore = f.FindeksScore

                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
