using Application.Dtos;
using Application.Features.CQRS.Queries;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest, ProductListDto>
    {
        private IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IGenericRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductListDto> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            var productDto = _mapper.Map<ProductListDto>(product);

            return productDto;
        }
    }
}
