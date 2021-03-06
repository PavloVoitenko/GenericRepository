﻿using GenericRepository.Abstractions.Entities;
using System;
using System.Threading.Tasks;

namespace GenericRepository.Abstractions.Repositories
{
    public interface INamedRepository<TEntity> : IRepository<TEntity> where TEntity : INamedEntity
    {
        Task<TEntity> CreateNameAsync(string name, Action<TEntity> initialize = null);
        TEntity FindName(string name);
        void DeleteName(string name);
        Task<TEntity> FindOrCreateAsync(string name, Action<TEntity> initialize = null);
    }
}
