using JWTApp.Back.Core.Application.Enums;
using JWTApp.Back.Core.Application.Features.CQRS.Commands;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Domain;
using MediatR;

namespace JWTApp.Back.Core.Application.Features.CQRS.Handlers
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
