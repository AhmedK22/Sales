using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Orders.Commands;
using Domain.Entities;
using Application.Interfaces;
using Domain.Specifications;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Order> _repo;
        private readonly IUnitOfWork _uow;
        public OrdersController(IMediator mediator, IRepository<Order> repo, IUnitOfWork uow)
        {
            _mediator = mediator;
            _repo = repo;
            _uow = uow;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand cmd)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if (cmd.CustomerId == Guid.Empty)
                return BadRequest("CustomerId is required.");
            if (cmd.Items == null || cmd.Items.Count == 0)
                return BadRequest("At least one item is required.");

            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // simple spec example
            
            ISpecification<Order>? spec = null;
            var list = await _repo.ListAsync(spec);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _repo.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _repo.GetByIdAsync(id);
            if (order == null) return NotFound();
            await _repo.DeleteAsync(order);
            await _uow.SaveChangesAsync();
            return NoContent();
        }
    }
}
