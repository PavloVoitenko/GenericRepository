using GenericRepository.Abstractions.Entities;

using System;
using System.Linq;

namespace GenericRepository.Abstractions.Repositories
{
    public interface IDateTimeStateRepository<TEntity> : IRepository<TEntity> where TEntity : IDateTimeEntity
    {
        public IQueryable<TEntity> AsOf(DateTime asOf = new DateTime());
        public IQueryable<TEntity> AsOfRange(DateTime from, DateTime to);
    }
}
