using JosiArchitecture.Core.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JosiArchitecture.Core.Users.Commands.CreateUser;
using JosiArchitecture.Core.Users.Queries.GetUsers;

namespace JosiArchitecture.Api.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetUsersResponse), 200)]
        public async Task<ActionResult<GetUsersResponse>> GetUsers([FromQuery] GetUsersRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetUserResponse), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetUserResponse>> GetUser(int id)
        {
            var response = await _mediator.Send(new GetUserRequest(id));
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var id = await _mediator.Send(request);
            return Created(Url.Action(nameof(GetUser), new { id })!, new { });
        }
    }
}