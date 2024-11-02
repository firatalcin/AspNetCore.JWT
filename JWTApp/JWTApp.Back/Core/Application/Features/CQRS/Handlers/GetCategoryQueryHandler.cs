using AutoMapper;
using JWTApp.Back.Core.Application.Dtos;
using JWTApp.Back.Core.Application.Features.CQRS.Queries;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Domain;
using MediatR;

namespace JWTApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, CategoryListDto>
    {
        private IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<CategoryListDto>(result);
        }
    }
}
