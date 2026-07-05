namespace OrderService.Application.Queries.GetOrders;
public sealed record GetOrdersQuery(int PageNumber, int PageSize, int CustomerId);