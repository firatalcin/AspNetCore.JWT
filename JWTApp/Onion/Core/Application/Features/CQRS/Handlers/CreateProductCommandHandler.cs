using Application.Features.CQRS.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IGenericRepository<Product> _repository;

        public CreateProductCommandHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Price = request.Price,
                Stock = request.Stock,
            };

            await _repository.CreateAsync(product);
        }
    }
}
