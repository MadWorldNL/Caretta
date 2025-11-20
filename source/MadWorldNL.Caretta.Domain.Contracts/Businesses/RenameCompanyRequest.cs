namespace MadWorldNL.Caretta.Businesses;

public class RenameCompanyRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}