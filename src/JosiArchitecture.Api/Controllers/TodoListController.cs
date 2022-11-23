using JosiArchitecture.Core.Todos.Commands.AddTodoList;
using JosiArchitecture.Core.Todos.Queries.GetTodoList;
using JosiArchitecture.Core.Todos.Queries.GetTodoLists;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JosiArchitecture.Api.Controllers
{
    [Route("todolists")]
    public class TodoListController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoListController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodoList(int id)
        {
            var response = await _mediator.Send(new GetTodoListRequest(id));

            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTodoList([FromBody] AddTodoListCommand request)
        {
            var id = await _mediator.Send(request);
            return Created(Url.Action(nameof(GetTodoList), new { id }), new { });
        }
    }
}