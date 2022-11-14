using JosiArchitecture.Core.Todos.Commands.AddTodoList;
using JosiArchitecture.Core.Todos.Queries.GetTodoList;
using JosiArchitecture.Core.Todos.Queries.GetTodoLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JosiArchitecture.Api.Controllers
{
    [Route("api/todolists")]
    public class TodoListController : Controller
    {
        private readonly IMediator _mediator;

        public TodoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetTodoListsResponse), 200)]
        public async Task<IActionResult> GetTodoLists()
        {
            var response = await _mediator.Send(new GetTodoListsRequest());
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetTodoListResponse), 200)]
        public async Task<IActionResult> GetTodo(int id)
        {
            var response = await _mediator.Send(new GetTodoListRequest(id));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodoListCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
