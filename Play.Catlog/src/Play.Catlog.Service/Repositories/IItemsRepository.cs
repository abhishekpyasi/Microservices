using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catlog.Service.Entities;

namespace Play.Catlog.Service.Repositories
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item entity);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetItemAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Item entity);
    }
}