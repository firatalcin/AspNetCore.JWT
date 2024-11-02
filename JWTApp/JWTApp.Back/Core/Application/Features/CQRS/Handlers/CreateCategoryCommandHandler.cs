using JWTApp.Back.Core.Application.Features.CQRS.Commands;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Domain;
using MediatR;

namespace JWTApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest>
    {
        private IGenericRepository<Category> _repository;

        public CreateCategoryCommandHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Category
            {
                Definition = request.Definition
            });
        }
    }
}
