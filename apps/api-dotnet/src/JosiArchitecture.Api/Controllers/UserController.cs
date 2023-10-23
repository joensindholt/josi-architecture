using JosiArchitecture.Core.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JosiArchitecture.Core.Users.Commands.CreateUser;
using JosiArchitecture.Core.Users.Queries.GetUsers;
using JosiArchitecture.Core.Users.Commands.DeleteUser;

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
        [ProducesResponseType(typeof(GetUsersResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetUsersResponse>> GetUsers([FromQuery] GetUsersRequest request)
        {
            System.Console.WriteLine("asd");
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetUserResponse>> GetUser(string id)
        {
            var response = await _mediator.Send(new GetUserRequest(id));
            return response.Match<ActionResult<GetUserResponse>>(
                userResponse => Ok(userResponse),
                notFound => NotFound());
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var response = await _mediator.Send(request);
            return Created(Url.Action(nameof(GetUser), new { response.Id })!, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await _mediator.Send(new DeleteUserRequest(id));

            return response.Match<IActionResult>(
                _ => NoContent(),
                notFound => NoContent()
            );
        }
    }
}
