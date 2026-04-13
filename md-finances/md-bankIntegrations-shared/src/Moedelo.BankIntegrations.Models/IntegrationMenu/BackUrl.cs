using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.Models.IntegrationMenu;

public class BackUrl
{
    public string RedirectUrl { get; set; }
    public IntegrationSource IntegrationSource { get; set; }
}