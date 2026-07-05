using System.Text.Json;
using OrderService.Domain.Events;

public class OutboxMessage
{
    public long Id {get; private set;}
    public Guid MessageId {get; private set;}
    public string? EventType {get; private set;}
    public string? Payload {get; private set;}
    public DateTime CreatedAt {get; private set;}
    public DateTime ProcessedAt {get; private set;}
    public bool IsProcesssed {get; private set;}

    public OutboxMessage(string? eventType, string payload)
    {
        MessageId = Guid.NewGuid();
        EventType = eventType;
        Payload = payload;
        CreatedAt = DateTime.UtcNow;
    }

    public void Processed()
    {
        ProcessedAt = DateTime.UtcNow;
        IsProcesssed = true;
    }

    public static OutboxMessage Create(IDomainEvent evt)
    {
        return new OutboxMessage(evt.GetType().FullName, JsonSerializer.Serialize((object)evt));
    }
}