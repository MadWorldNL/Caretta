using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public record CompanyRenamedEvent(UniqueId CompanyId, Name Name, UpdatedTime UpdatedAt) : IDomainEvent;