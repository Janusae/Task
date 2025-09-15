using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskService.Application.CQRS.Command.Tasks;

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
        public IActionResult GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Create([FromBody] object request)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] object request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
