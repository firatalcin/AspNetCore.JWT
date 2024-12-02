using JWTApp.Back.Core.Application.Features.CQRS.Commands;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Domain;
using MediatR;

namespace JWTApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IGenericRepository<Category> _repository;

        public UpdateCategoryCommandHandler(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category != null)
            {
                category.Id = request.Id;
                category.Definition = request.Definition;

                await _repository.UpdateAsync(category);
            }
        }
    }
}
