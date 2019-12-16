using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GenericRepository.Implementations
{
    public class TimeStateRepository<TEntity> : Repository<TEntity>, IRangeStateRepository<TEntity, TimeSpan>
        where TEntity : class, IRangeEntity<TimeSpan>
    {
        public TimeStateRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<TEntity> GetAsOf(TimeSpan asOf = new TimeSpan())
        {
            return GetAsOfRange(asOf, asOf);
        }
        public IQueryable<TEntity> GetAsOfRange(TimeSpan from, TimeSpan to)
        {
            return Db.Set<TEntity>().Where(e => e.ValidFrom <= to && e.ValidTo >= from);
        }

        public override IQueryable<TEntity> Get()
        {
            return GetAsOf();
        }
    }
}
