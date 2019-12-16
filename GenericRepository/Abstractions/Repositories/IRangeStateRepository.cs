using GenericRepository.Abstractions.Entities;
using System.Linq;

namespace GenericRepository.Abstractions.Repositories
{
    public interface IRangeStateRepository<TEntity, TRange> where TEntity : IRangeEntity<TRange>
    {
        IQueryable<TEntity> GetAsOf(TRange asOf);
        IQueryable<TEntity> GetAsOfRange(TRange from, TRange to);
    }
}
