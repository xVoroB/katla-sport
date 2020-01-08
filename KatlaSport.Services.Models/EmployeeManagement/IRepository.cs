using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetEntityAsync(int id);

        Task<List<TEntity>> GetAllEntitiesAsync();

        Task AddEntityAsync(TEntity entity);

        Task UpdateEntityAsync(TEntity entity);

        Task RemoveEntityAsync(TEntity entity);
    }
}
