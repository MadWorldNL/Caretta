using System.Text.Json;
using KurrentDB.Client;

namespace MadWorldNL.Caretta.EventStorages;

public class EventDbContext(KurrentDBClient client) : IEventStorage
{
    public async Task<TRootAggregate> GetById<TRootAggregate>(Guid id) where TRootAggregate : RootAggregate
    {
        var rootAggregate = (TRootAggregate)Activator.CreateInstance(typeof(TRootAggregate), true)!;

        var resolvedEvents = await client.ReadStreamAsync(
            Direction.Forwards, $"{rootAggregate.AggregateType}-{id}", StreamPosition.Start
        ).ToListAsync();

        foreach (var resolvedEvent in resolvedEvents)
        {
            var @event = JsonSerializer.Deserialize(
                resolvedEvent.Event.Data.Span,
                Type.GetType(resolvedEvent.Event.EventType)!);

            if (@event is IDomainEvent domainEvent)
            {
                rootAggregate.Apply(domainEvent);   
            }
        }

        return rootAggregate;
    }
    
    public async Task Store(RootAggregate aggregate)
    {
        var streamName = aggregate.AggregateId;
        var events = aggregate.DomainEvents.Select(domainEvent => 
            new EventData(
                Uuid.NewUuid(),
                $"{domainEvent.GetType().FullName}, {domainEvent.GetType().Assembly.GetName().Name}",
                JsonSerializer.SerializeToUtf8Bytes(domainEvent, domainEvent.GetType()),
                null));
        await client.AppendToStreamAsync(streamName, StreamState.Any, events);
    
        aggregate.ClearDomainEvents();
    }
}