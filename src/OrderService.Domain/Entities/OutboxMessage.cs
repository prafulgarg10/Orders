using System.Text.Json;
using OrderService.Domain.Events;

public class OutboxMessage
{
    public long Id {get; private set;}
    public Guid MessageId {get; private set;}
    public string EventType {get; private set;} = string.Empty;
    public string Payload {get; private set;} = string.Empty;
    public DateTime CreatedAt {get; private set;}
    public DateTime? ProcessedAt {get; private set;}
    public bool IsProcessed {get; private set;}
    public string RoutingKey {get; private set;} = string.Empty;

    private OutboxMessage(string eventType, string payload, string routingKey)
    {
        MessageId = Guid.NewGuid();
        EventType = eventType;
        Payload = payload;
        CreatedAt = DateTime.UtcNow;
        RoutingKey = routingKey;
    }

    public void MarkAsProcessed()
    {
        ProcessedAt = DateTime.UtcNow;
        IsProcessed = true;
    }

    public static OutboxMessage Create(IDomainEvent evt, string routingKey)
    {
        string? eventType = evt.GetType().Name;
        return new OutboxMessage(eventType != null ? eventType : string.Empty, JsonSerializer.Serialize((object)evt), routingKey);
    }
}