using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catlog.Service.Entities;

namespace Play.Catlog.Service.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetItemAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}