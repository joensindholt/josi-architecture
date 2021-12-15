using JosiArchitecture.Core.Todos.Commands;
using JosiArchitecture.Core.Todos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

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
        [ProducesResponseType(typeof(GetTodosResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodos(GetTodosRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetTodosResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostTodo(AddTodoCommand request)
        {
            var response = await _mediator.Send(request);
            return Created(Url.Action(nameof(GetTodo), new { id = response.Id }), response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetTodosResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodo(long id)
        {
            var response = await _mediator.Send(new GetTodoRequest(id));
            return response != null ? Ok(response) : NotFound();
        }
    }
}