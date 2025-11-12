using System.Text.Json;
using KurrentDB.Client;

namespace MadWorldNL.Caretta.EventStorages;

public class EventDbContext(KurrentDBClient client) : IEventStorage
{
    public async Task<TRootAggregate> GetById<TRootAggregate>(Guid id) where TRootAggregate : RootAggregate
    {
        var rootAggregate = (TRootAggregate)Activator.CreateInstance(typeof(TRootAggregate), true)!;
        
        var stream = client.ReadStreamAsync(
            Direction.Forwards, $"{rootAggregate}-{id}", StreamPosition.Start
        );

        await foreach (var resolved in stream)
        {
            var @event = JsonSerializer.Deserialize(
                resolved.Event.Data.Span,
                Type.GetType(resolved.Event.EventType)!);

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
                domainEvent.GetType().Name,
                JsonSerializer.SerializeToUtf8Bytes(domainEvent),
                null));
        await client.AppendToStreamAsync(streamName, StreamState.Any, events);
    
        aggregate.ClearDomainEvents();
    }
}