using Application.Dtos;
using Application.Features.CQRS.Queries;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CQRS.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IGenericRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ProductListDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync();
            return mapper.Map<List<ProductListDto>>(list);
        }
    }
}
