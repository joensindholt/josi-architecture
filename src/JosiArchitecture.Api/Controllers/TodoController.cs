using System.Threading.Tasks;
using JosiArchitecture.Core.Todos.Commands;
using JosiArchitecture.Core.Todos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JosiArchitecture.Api.Controllers
{
    [Route("api/todos")]
    public class TodoController : Controller
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetTodosResponse), 200)]
        public async Task<IActionResult> Get(GetTodosRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post(AddTodoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
