using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GenericRepository.Implementations
{
    public class DateRangeRepository<TEntity> : Repository<TEntity>, IRangeRepository<TEntity, DateTime>
        where TEntity : class, IRangeEntity<DateTime>
    {
        public DateRangeRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<TEntity> On(DateTime asOf = new DateTime())
        {
            return Between(asOf, asOf);
        }
        public IQueryable<TEntity> Between(DateTime from, DateTime to)
        {
            return Db.Set<TEntity>().Where(e => e.RangeFrom <= to && e.RangeTo >= from);
        }

        public override IQueryable<TEntity> Get()
        {
            return On();
        }
    }
}
