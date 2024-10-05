using SharedLibrary.Dtos;
using System.Linq.Expressions;

namespace AuthServer.Core.Services
{
    public interface IGenericService<TEntity,TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(Guid id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<TDto>> AddAsync(TEntity entity);
        Task<Response<NoDataDto>> Remove(TEntity entity);
        Task<Response<NoDataDto>> Update(TEntity entity);
    }
}
