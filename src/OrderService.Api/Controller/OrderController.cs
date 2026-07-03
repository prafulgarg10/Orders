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
    public async Task<IActionResult> Orders()
    {
        List<OrderResponse> response = await _orderService.GetAllOrders();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Orders([FromRoute] int id)
    {
        OrderResponse? response = await _orderService.GetOrderDetail(id);
        if (response != null)
        {
            return Ok(response);
        }
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Orders([FromBody] OrderRequest orderRequest)
    {
        OrderResponse? response = await _orderService.PlaceOrder(orderRequest);
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