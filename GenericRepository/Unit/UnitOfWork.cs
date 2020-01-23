using GenericRepository.Abstractions.Entities;
using GenericRepository.Abstractions.Repositories;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericRepository.Unit
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repo<TEntity>() where TEntity : class, IEntity, new();
        TRepo Repo<TEntity, TRepo>()
            where TRepo : IRepository<TEntity>
            where TEntity : class, IEntity, new();

        IQueryable<TEntity> All<TEntity>()
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
            return Repo<TEntity, IRepository<TEntity>>();
        }

        public TRepo Repo<TEntity, TRepo>()
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

        public IQueryable<TEntity> All<TEntity>()
            where TEntity : class, IEntity, new()
        {
            return Repo<TEntity>().Get();
        }
    }
}
