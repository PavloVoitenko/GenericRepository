using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected DbContext Db { get; set; }

        public Repository(DbContext context)
        {
            Db = context;
        }

        public virtual void Create(params TEntity[] entities)
        {
            Db.AddRange(entities);
        }
        public virtual void Delete(params TEntity[] entities)
        {
            Db.RemoveRange(entities);
        }
        public virtual IQueryable<TEntity> Get()
        {
            return Db.Set<TEntity>();
        }
        public virtual void Update(params TEntity[] entities)
        {
            Db.UpdateRange(entities);
        }

        public async Task Commit()
        {
            await Db.SaveChangesAsync();
        }
    }
}
