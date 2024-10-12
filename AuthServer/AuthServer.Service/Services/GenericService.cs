using AuthServer.Core.Models;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using System.Linq.Expressions;

namespace AuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return Response<TDto>.Success(newDto, 200, true);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _repository.GetAllAsync());

            return Response<IEnumerable<TDto>>.Success(products, 200, true);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);   

            if (product == null)
            {
                return Response<TDto>.Fail(new ErrorDto("Id not found", true), 404, false);
            }

            var newDto = ObjectMapper.Mapper.Map<TDto>(product);

            return Response<TDto>.Success(newDto, 200, true);
        }      

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);

            if(isExistEntity == null)
            {
                return Response<NoDataDto>.Fail(new ErrorDto("Id not found", true), 404, false);
            }

            _repository.Remove(isExistEntity);
            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204, true);
        }

        public async Task<Response<NoDataDto>> Update(TDto entity, int id)
        {
            var isExistEntity = await _repository.GetByIdAsync(id);

            if (isExistEntity == null)
            {
                return Response<NoDataDto>.Fail(new ErrorDto("Id not found", true), 404, false);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _repository.Update(updateEntity);
            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(204, true);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Where(predicate);

            var newDtoList = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync());

            return Response<IEnumerable<TDto>>.Success(newDtoList, 200, true);

        }
    }
}
