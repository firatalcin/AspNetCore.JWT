using JWTApp.Back.Core.Application.Features.CQRS.Commands;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Domain;
using MediatR;

namespace JWTApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IGenericRepository<Product> _repository;

        public UpdateProductCommandHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product != null)
            {
                product.Name = request.Name;
                product.Price = request.Price;
                product.CategoryId = request.CategoryId;
                product.Stock = request.Stock;

                await _repository.UpdateAsync(product);
            }
        }
    }
}
