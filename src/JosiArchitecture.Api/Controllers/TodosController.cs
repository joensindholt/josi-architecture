using System.Collections.Generic;
using System.Threading.Tasks;
using JosiArchitecture.Core.Todos;
using JosiArchitecture.Core.Todos.Commands;
using JosiArchitecture.Core.Todos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JosiArchitecture.Api.Controllers
{
    [Route("api/todos")]
    public class TodosController : Controller
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetTodosRequest request)
        {
            IEnumerable<Todo> todos = await _mediator.Send(request);
            return Ok(todos);
        }

        // GET api/todos/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return NotFound();
        }

        // POST api/todos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddTodoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        // PUT api/todos/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return NotFound();
        }

        // DELETE api/todos/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NotFound();
        }
    }
}
