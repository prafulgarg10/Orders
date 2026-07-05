namespace OrderService.Application.Queries;
public sealed record GetQueryResult(int OrderId, decimal Amount, string Status, DateTime CreatedAt);