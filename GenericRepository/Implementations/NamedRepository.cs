using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task DeleteNameAsync(string name)
        {
            var entity = await FindNameAsync(name);

            if (entity != null)
            {
                Delete(entity);
            }
        }
        public async Task<TEntity> FindNameAsync(string name)
        {
            return await Db.Set<TEntity>().FirstOrDefaultAsync(e => e.Name == name);
        }
        public async Task<TEntity> FindOrCreateAsync(string name, Action<TEntity> initialize = null)
        {
            return await FindNameAsync(name) ?? await CreateNameAsync(name, initialize);
        }
    }
}
