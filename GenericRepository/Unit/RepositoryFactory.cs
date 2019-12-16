using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;
using GenericRepository.Implementations;
using Microsoft.EntityFrameworkCore;
using System;

namespace GenericRepository.Unit
{
    public interface IRepositoryFactory
    {
        IRepository Create<TEntity>(DbContext context) where TEntity : class, IEntity, new();
    }

    public abstract class RepositoryFactory : IRepositoryFactory
    {
        public IRepository Create<TEntity>(DbContext context)
            where TEntity : class, IEntity, new()
        {
            return CreateCustom(context) ?? CreateDefault<TEntity>(context);
        }

        public abstract IRepository CreateCustom(DbContext context);

        private IRepository CreateDefault<TEntity>(DbContext context)
            where TEntity : class, IEntity, new()
        {
            switch (new TEntity())
            {
                case IRangeEntity<DateTime> _:
                    return CreateGenericRepository<TEntity>(typeof(DateStateRepository<>), context);
                case IRangeEntity<TimeSpan> _:
                    return CreateGenericRepository<TEntity>(typeof(TimeStateRepository<>), context);
                case INamedEntity _:
                    return CreateGenericRepository<TEntity>(typeof(NamedRepository<>), context);
                default:
                    return new Repository<TEntity>(context);
            }
        }

        private IRepository CreateGenericRepository<TEntity>(Type repositoryType, DbContext context)
        {
            var genericRepoType = repositoryType.MakeGenericType(typeof(TEntity));
            return (IRepository)Activator.CreateInstance(genericRepoType, context);
        }
    }
}
