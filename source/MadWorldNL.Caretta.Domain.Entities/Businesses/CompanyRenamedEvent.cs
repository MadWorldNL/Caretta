using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public record CompanyRenamedEvent(Name Name, UpdatedTime UpdatedAt) : IDomainEvent;