using MadWorldNL.Caretta.Businesses.ValueObjects;
using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public record CompanyFoundedEvent(UniqueId CompanyId, Name Name, FoundedTime FoundedAt) : IDomainEvent;
