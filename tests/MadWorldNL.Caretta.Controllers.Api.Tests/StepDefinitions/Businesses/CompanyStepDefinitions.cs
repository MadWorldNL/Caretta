namespace MadWorldNL.Caretta.StepDefinitions.Businesses;

[Binding]
public class CompanyStepDefinitions
{
    [Given("I have chosen the company name {string}")]
    public void GivenIHaveChosenTheCompanyName(string companyName)
    {
        throw new PendingStepException();
    }

    [When("I found the company")]
    public void WhenIFoundTheCompany()
    {
        throw new PendingStepException();
    }

    [Then("the company {string} should be created")]
    public void ThenTheCompanyShouldBeCreated(string companyName)
    {
        throw new PendingStepException();
    }
}