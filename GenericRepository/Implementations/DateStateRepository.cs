using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GenericRepository.Implementations
{
    public class DateStateRepository<TEntity> : Repository<TEntity>, IRangeStateRepository<TEntity, DateTime>
        where TEntity : class, IRangeEntity<DateTime>
    {
        public DateStateRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<TEntity> GetAsOf(DateTime asOf = new DateTime())
        {
            return GetAsOfRange(asOf, asOf);
        }
        public IQueryable<TEntity> GetAsOfRange(DateTime from, DateTime to)
        {
            return Db.Set<TEntity>().Where(e => e.ValidFrom <= to && e.ValidTo >= from);
        }

        public override IQueryable<TEntity> Get()
        {
            return GetAsOf();
        }
    }
}
