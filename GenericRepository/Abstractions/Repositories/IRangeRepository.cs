using GenericRepository.Abstractions.Entities;
using System.Linq;

namespace GenericRepository.Abstractions.Repositories
{
    public interface IRangeRepository<TEntity, TRange> : IRepository<TEntity> where TEntity : IRangeEntity<TRange>
    {
        IQueryable<TEntity> On(TRange asOf);
        IQueryable<TEntity> Between(TRange from, TRange to);
    }
}
