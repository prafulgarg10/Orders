using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Queries;

[Route("/")]
public class OrderController : ControllerBase
{
    private CreateOrderHandler _handler;
    private GetQueryHandler _queryHandler;
    public OrderController(CreateOrderHandler handler, GetQueryHandler queryHandler)
    {
        _handler = handler;
        _queryHandler = queryHandler;
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
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetOrderQuery(id);
        var result = await _queryHandler.HandleAsync(query, cancellationToken);
        return Ok(result);
    }
}