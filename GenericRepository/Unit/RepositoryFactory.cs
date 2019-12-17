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

    public abstract class RepositoryFactoryBase : IRepositoryFactory
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
            return (new TEntity()) switch
            {
                IRangeEntity<DateTime> _ => CreateGenericRepository<TEntity>(typeof(DateStateRepository<>), context),
                IRangeEntity<TimeSpan> _ => CreateGenericRepository<TEntity>(typeof(TimeStateRepository<>), context),
                INamedEntity _ => CreateGenericRepository<TEntity>(typeof(NamedRepository<>), context),
                _ => new Repository<TEntity>(context),
            };
        }

        private IRepository CreateGenericRepository<TEntity>(Type repositoryType, DbContext context)
        {
            var genericRepoType = repositoryType.MakeGenericType(typeof(TEntity));
            return (IRepository)Activator.CreateInstance(genericRepoType, context);
        }
    }

    public class RepositoryFactory : RepositoryFactoryBase
    {
        public override IRepository CreateCustom(DbContext context) => null;
    }
}
