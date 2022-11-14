using JosiArchitecture.Core.Todos.Commands.AddTodo;
using JosiArchitecture.Core.Todos.Queries.GetTodo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace JosiArchitecture.Api.Controllers
{
    [Route("api/todolist/{todoListId}/todo")]
    public class TodoController : Controller
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddTodoCommand), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddTodo(AddTodoCommand request)
        {
            var response = await _mediator.Send(request);
            return Created(Url.Action(nameof(GetTodo), new { id = response.Id }), response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetTodoResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTodo(long todoListId, long id)
        {
            var response = await _mediator.Send(new GetTodoRequest(todoListId, id));
            return response != null ? Ok(response) : NotFound();
        }
    }
}
