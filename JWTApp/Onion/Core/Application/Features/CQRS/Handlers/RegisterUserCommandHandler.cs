using Application.Enums;
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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IGenericRepository<AppUser> _repository;

        public RegisterUserCommandHandler(IGenericRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new AppUser
            {
                Username = request.UserName,
                Password = request.Password,
                AppRoleId = (int)RoleType.Member,
            });
        }
    }
}
