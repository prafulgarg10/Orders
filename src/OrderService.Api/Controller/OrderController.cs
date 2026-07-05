using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("/")]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Orders(CancellationToken cancellationToken)
    {
        List<OrderResponse> response = await _orderService.GetAllOrders(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Orders([FromRoute] int id, CancellationToken cancellationToken)
    {
        OrderResponse? response = await _orderService.GetOrderDetail(id, cancellationToken);
        if (response != null)
        {
            return Ok(response);
        }
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Orders([FromBody] OrderRequest orderRequest, CancellationToken cancellationToken)
    {
        OrderResponse? response = await _orderService.PlaceOrder(orderRequest, cancellationToken);
        if (response != null)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest();
        }
    }
}