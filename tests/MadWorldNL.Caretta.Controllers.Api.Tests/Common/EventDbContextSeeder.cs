using System.Text.Json;
using KurrentDB.Client;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Common;

public class EventDbContextSeeder(KurrentDBClient client)
{
    public async Task Store(string aggregateId, IDomainEvent[] domainEvents)
    {
        var events = domainEvents.Select(domainEvent => 
            new EventData(
                Uuid.NewUuid(),
                $"{domainEvent.GetType().FullName}, {domainEvent.GetType().Assembly.GetName().Name}",
                JsonSerializer.SerializeToUtf8Bytes(domainEvent, domainEvent.GetType()),
                null));
        
        await client.AppendToStreamAsync(aggregateId, StreamState.Any, events);
    }
}