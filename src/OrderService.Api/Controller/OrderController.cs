using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Queries.GetOrder;
using OrderService.Application.Queries.GetOrders;

[Route("/")]
public class OrderController : ControllerBase
{
    private CreateOrderHandler _createOrderHandler;
    private GetOrderHandler _getOrderHandler;
    private GetOrdersHandler _getOrdersHandler;
    public OrderController(CreateOrderHandler createOrderHandler, GetOrderHandler getOrderHandler, GetOrdersHandler getOrdersHandler)
    {
        _createOrderHandler = createOrderHandler;
        _getOrderHandler = getOrderHandler;
        _getOrdersHandler = getOrdersHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery getOrdersQuery, CancellationToken cancellationToken)
    {
        var response = await _getOrdersHandler.HandleAsync(getOrdersQuery, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(orderRequest.CustomerId, orderRequest.Amount);
        var result = await _createOrderHandler.HandleAsync(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new {id = result.OrderNumber}, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetOrderQuery(id);
        var result = await _getOrderHandler.HandleAsync(query, cancellationToken);
        return Ok(result);
    }
}