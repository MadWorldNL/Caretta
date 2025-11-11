using System.Text.Json;
using KurrentDB.Client;

namespace MadWorldNL.Caretta.EventStorages;

public class EventDbContext(KurrentDBClient client) : IEventStorage
{
    public async Task Store(RootAggregate aggregate)
    {
        var streamName = $"{aggregate.AggregateType}-{aggregate.Id}";
        var events = aggregate.DomainEvents.Select(domainEvent => 
            new EventData(
                Uuid.NewUuid(),
                domainEvent.GetType().Name,
                JsonSerializer.SerializeToUtf8Bytes(domainEvent),
                null));
        await client.AppendToStreamAsync(streamName, StreamState.Any, events);
    
        aggregate.ClearDomainEvents();
    }
}