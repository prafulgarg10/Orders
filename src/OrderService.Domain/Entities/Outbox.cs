public class Outbox
{
    public int Id {get; private set;}
    public Guid MessageId {get; private set;}
    public Event EventType {get; private set;}
    public string? Payload {get; private set;}
    public DateTime CreatedAt {get; private set;}
    public DateTime ProcessedAt {get; private set;}
    public bool IsProcesssed {get; private set;}

    public Outbox(Event eventType, string payload)
    {
        MessageId = new Guid();
        EventType = eventType;
        Payload = payload;
        CreatedAt = DateTime.UtcNow;
    }

    public void Processed()
    {
        ProcessedAt = DateTime.UtcNow;
        IsProcesssed = true;
    }
}