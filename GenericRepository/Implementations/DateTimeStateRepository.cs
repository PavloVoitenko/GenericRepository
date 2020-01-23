using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GenericRepository.Implementations
{
    public class DateTimeStateRepository<TEntity> : Repository<TEntity>, IDateTimeStateRepository<TEntity> where TEntity : class, IDateTimeEntity
    {
        public DateTimeStateRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<TEntity> AsOf(DateTime asOf = new DateTime())
        {
            return AsOfRange(asOf, asOf);
        }
        public IQueryable<TEntity> AsOfRange(DateTime from, DateTime to)
        {
            var fromDate = from.Date;
            var fromTime = from.TimeOfDay;
            var toDate = to.Date;
            var toTime = new TimeSpan(23, 59, 59);

            return Db.Set<TEntity>().Where(e =>
                (fromDate < e.AsOfDate && toDate > e.AsOfDate) ||
                (fromDate == e.AsOfDate && fromTime <= e.RangeTo) ||
                (toDate == e.AsOfDate && toTime >= e.RangeFrom));
        }

        public override IQueryable<TEntity> Get()
        {
            return AsOf();
        }
    }
}
