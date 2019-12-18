using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace KatlaSport.Services.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetEntityAsync(int id);

        Task<List<TEntity>> GetAllEntitiesAsync();

        Task<IHttpActionResult> AddEntityAsync(TEntity entity);

        Task<IHttpActionResult> UpdateEntityAsync(TEntity entity);

        Task<IHttpActionResult> RemoveEntityAsync(TEntity entity);
    }
}
