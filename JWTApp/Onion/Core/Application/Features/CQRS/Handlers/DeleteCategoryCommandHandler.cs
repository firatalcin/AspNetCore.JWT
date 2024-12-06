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
