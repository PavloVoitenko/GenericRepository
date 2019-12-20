using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository.Implementations
{
    public class NamedRepository<TEntity> : Repository<TEntity>, INamedRepository<TEntity>
        where TEntity : class, INamedEntity, new()
    {
        public NamedRepository(DbContext context) : base(context)
        {
        }

        public async Task<TEntity> CreateNameAsync(string name, Action<TEntity> initialize = null)
        {
            var entity = new TEntity
            {
                Name = name
            };
            initialize?.Invoke(entity);

            Create(entity);

            await Commit();
            return entity;
        }
        public void DeleteName(string name)
        {
            var entity = FindName(name);

            if (entity != null)
            {
                Delete(entity);
            }
        }
        public TEntity FindName(string name)
        {
            return Db.Set<TEntity>().FirstOrDefault(e => e.Name == name);
        }
        public async Task<TEntity> FindOrCreateAsync(string name, Action<TEntity> initialize = null)
        {
            return FindName(name) ?? await CreateNameAsync(name, initialize);
        }
    }
}
