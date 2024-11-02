using AutoMapper;
using JWTApp.Back.Core.Application.Dtos;
using JWTApp.Back.Core.Application.Features.CQRS.Queries;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Domain;
using MediatR;

namespace JWTApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, List<CategoryListDto>>
    {
        private IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<CategoryListDto>>(result);
        }
    }
}
