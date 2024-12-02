using JWTApp.Back.Core.Application.Features.CQRS.Commands;
using JWTApp.Back.Core.Application.Features.CQRS.Queries;
using JWTApp.Back.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JWTApp.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(CheckUserQueryRequest request)
        {
            var dto = await _mediator.Send(request);
            if (dto.IsExist)
            {
                return Created("", JwtTokenGenerator.GenerateToken(dto));
            }
            else
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı");
            }
        }
    }
}
