using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public record CompanyFoundedEvent(UniqueId CompanyId, string Name, DateTime FoundedAt) : IDomainEvent;
