using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskService.Application.CQRS.Command.Tasks;
using TaskService.Application.Dto;

namespace TaskService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetTaskQuery { });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery {Id=id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto request)
        {
            var result = await _mediator.Send(new CreateTaskQuery { CreateUserDto = request });
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskDto request)
        {
            var result = await _mediator.Send(new UpdateTaskQuery { UpdateTaskDto = request });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTaskQuery { Id = id });
            return Ok(result);
        }
    }
}
