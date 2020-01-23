using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GenericRepository.Implementations
{
    public class TimeRangeRepository<TEntity> : Repository<TEntity>, IRangeRepository<TEntity, TimeSpan>
        where TEntity : class, IRangeEntity<TimeSpan>
    {
        public TimeRangeRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<TEntity> On(TimeSpan asOf = new TimeSpan())
        {
            return Between(asOf, asOf);
        }
        public IQueryable<TEntity> Between(TimeSpan from, TimeSpan to)
        {
            return Db.Set<TEntity>().Where(e => e.RangeFrom <= to && e.RangeTo >= from);
        }

        public override IQueryable<TEntity> Get()
        {
            return On();
        }
    }
}
