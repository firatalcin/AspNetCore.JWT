using JWTApp.Back.Core.Application.Features.CQRS.Commands;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Domain;
using MediatR;

namespace JWTApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest>
    {
        private readonly IGenericRepository<Category> _repository;

        public DeleteCategoryCommandHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category != null)
            {
                await _repository.RemoveAsyc(category);
            }
        }
    }
}
