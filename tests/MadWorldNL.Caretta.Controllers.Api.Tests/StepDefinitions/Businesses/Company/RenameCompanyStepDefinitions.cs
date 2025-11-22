using System.Net.Http.Json;
using MadWorldNL.Caretta.Businesses;
using MadWorldNL.Caretta.Businesses.ValueObjects;
using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;
using Shouldly;

namespace MadWorldNL.Caretta.StepDefinitions.Businesses.Company;

[Binding]
public class RenameCompanyStepDefinitions(ApiWebApplicationFactory factory) : IClassFixture<ApiWebApplicationFactory>
{
    private readonly Guid _existingCompanyId = Guid.Parse("950373a7-f058-492a-95f3-bfe01087def5");
    private string _newCompanyName = string.Empty;
    
    [Given("an existing company named {string}")]
    public async Task GivenAnExistingCompanyNamed(string companyName)
    {
        var seeder = factory.GetSeeder();
        
        var foundedCompanyEvent = new CompanyFoundedEvent(
            new UniqueId(_existingCompanyId), 
                new Name(companyName), 
                new FoundedTime(new DateTime(2025, 10, 10, 10, 10, 0, DateTimeKind.Utc)), 
                new UpdatedTime(new DateTime(2025, 10, 10, 10, 10, 0, DateTimeKind.Utc)));

        await seeder.Store($"Company-{_existingCompanyId}", [foundedCompanyEvent]);
    }

    [Given("I have chosen the new company name {string}")]
    public void GivenIHaveChosenTheNewCompanyName(string companyName)
    {
        _newCompanyName = companyName;
    }

    [When("I rename the company")]
    public async Task WhenIRenameTheCompany()
    {
        using var client = factory.CreateClient();

        var request = new RenameCompanyRequest(_existingCompanyId, _newCompanyName);
        var response = await client.PostAsJsonAsync("/Company/RenameCompany", request);

        if (!response.IsSuccessStatusCode)
        {
            throw new TestFailedException("Failed to rename company");
        }
    }

    [Then("the company should be named {string}")]
    public async Task ThenTheCompanyShouldBeNamed(string companyName)
    {
        using var client = factory.CreateClient();
        
        var response = await client.GetFromJsonAsync<LoadCompanyResponse>($"/Company/Load?id={_existingCompanyId}");
        response.ShouldNotBeNull();
        response.Name.ShouldBe(companyName);
    }
}