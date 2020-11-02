using System.Threading.Tasks;
using JosiArchitecture.Core.Todos.Commands;
using JosiArchitecture.Core.Todos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetTodoListsRequest());
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetTodoListResponse), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetTodoListRequest(id));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddTodoListCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
