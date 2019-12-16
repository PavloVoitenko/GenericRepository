using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GenericRepository.Unit
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repo<TEntity>() where TEntity : class, IEntity, new();
        TRepo Repo<TRepo, TEntity>()
            where TRepo : IRepository<TEntity>
            where TEntity : class, IEntity, new();
    }

    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IRepositoryFactory _factory;

        private readonly IDictionary<Type, IRepository> _repositories;

        public UnitOfWork(TContext context, IRepositoryFactory repoitoryFactory)
        {
            _context = context;
            _factory = repoitoryFactory;

            _repositories = new Dictionary<Type, IRepository>();
        }

        public IRepository<TEntity> Repo<TEntity>() where TEntity : class, IEntity, new()
        {
            return Repo<IRepository<TEntity>, TEntity>();
        }

        public TRepo Repo<TRepo, TEntity>()
            where TRepo : IRepository<TEntity>
            where TEntity : class, IEntity, new()
        {
            var entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
            {
                _repositories.Add(entityType, _factory.Create<TEntity>(_context));
            }

            return (TRepo)_repositories[entityType];
        }
    }
}
