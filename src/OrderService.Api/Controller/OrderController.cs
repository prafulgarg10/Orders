using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands.CreateOrder;

[Route("/")]
public class OrderController : ControllerBase
{
    private CreateOrderHandler _handler;
    public OrderController(CreateOrderHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(orderRequest.CustomerId, orderRequest.Amount);
        var result = await _handler.HandleAsync(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new {id = result.OrderNumber}, result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }
}