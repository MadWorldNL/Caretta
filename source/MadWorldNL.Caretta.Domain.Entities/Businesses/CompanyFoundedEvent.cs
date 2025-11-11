using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public record CompanyFoundedEvent(Guid CompanyId, string Name, DateTime FoundedAt) : IDomainEvent;
