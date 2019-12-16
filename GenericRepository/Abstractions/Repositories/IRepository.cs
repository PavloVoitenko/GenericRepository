using GenericRepository.Abstractions.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.Abstractions.Repositories
{
    public interface IRepository
    {
        Task Commit();
    }

    public interface IRepository<TEntity> : IRepository
        where TEntity : IEntity
    {
        IQueryable<TEntity> Get();
        void Create(params TEntity[] entities);
        void Update(params TEntity[] entities);
        void Delete(params TEntity[] entities);
    }
}
