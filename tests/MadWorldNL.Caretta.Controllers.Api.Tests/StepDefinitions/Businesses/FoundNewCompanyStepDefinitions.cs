using System.Net.Http.Json;
using MadWorldNL.Caretta.Businesses;
using MadWorldNL.Caretta.Common;
using MadWorldNL.Caretta.Default;
using Shouldly;

namespace MadWorldNL.Caretta.StepDefinitions.Businesses;

[Binding]
public class FoundNewCompanyStepDefinitions(ApiWebApplicationFactory factory) : IClassFixture<ApiWebApplicationFactory>
{
    private string _companyName = string.Empty;
    private Guid _companyId = Guid.Empty;
    
    [Given("I have chosen the company name {string}")]
    public void GivenIHaveChosenTheCompanyName(string companyName)
    {
        _companyName = companyName;
    }

    [When("I found the company")]
    public async Task WhenIFoundTheCompany()
    {
        using var client = factory.CreateClient();

        var request = new StartNewCompanyRequest(_companyName);
        var response = await client.PostAsJsonAsync("/Company/StartNewCompany", request);

        if (response.IsSuccessStatusCode)
        {
            var changedResponse = await response.Content.ReadFromJsonAsync<ChangedResponse>();
            _companyId = changedResponse!.ObjectChangedId;
        }
    }

    [Then("the company {string} should be created")]
    public async Task ThenTheCompanyShouldBeCreated(string companyName)
    {
        using var client = factory.CreateClient();
        
        var response = await client.GetFromJsonAsync<LoadCompanyResponse>($"/Company/Load?id={_companyId}");
        response.ShouldNotBeNull();
        response.Name.ShouldBe(companyName);
    }
}