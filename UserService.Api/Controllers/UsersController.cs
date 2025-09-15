using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.Application.CQRS.Command.Users;
using UserService.Application.CQRS.Users;
using UserService.Application.Dto;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediatr;
        public UsersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _mediatr.Send(new GetAllUsersQuery());
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _mediatr.Send(new GetUserByIdQuery { Id = id });
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto request)
        {
            var user = await _mediatr.Send(new CreateUserCommand { CreateUserDto = request });
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto request)
        {
            var user = await _mediatr.Send(new UpdateUserCommand { UpdateUserDto = request });
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _mediatr.Send(new DeleteUserCommand { Id = id });
            return Ok(user);
        }
    }
}
