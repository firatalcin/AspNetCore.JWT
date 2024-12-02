using JWTApp.Back.Core.Application.Dtos;
using JWTApp.Back.Core.Application.Features.CQRS.Queries;
using JWTApp.Back.Core.Application.Interfaces;
using JWTApp.Back.Core.Domain;
using MediatR;

namespace JWTApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class CheckUserRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
    {
        private readonly IGenericRepository<AppUser> _userRepository;
        private readonly IGenericRepository<AppRole> _roleRepository;

        public CheckUserRequestHandler(IGenericRepository<AppRole> roleRepository, IGenericRepository<AppUser> userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto = new CheckUserResponseDto();

            var user = await _userRepository.GetByFilterAsync(x => x.Username == request.Username && x.Password == request.Password);

            if (user == null)
            {
                dto.IsExist = false;
            }
            else
            {
                dto.Username = user.Username;
                dto.Id = user.Id;
                dto.IsExist = true;

                var role = await _roleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);

                dto.Role = role?.Definition;

            }

            return dto;
        }
    }
}
